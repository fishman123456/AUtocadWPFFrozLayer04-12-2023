﻿using AcadApp = Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Autodesk.AutoCAD.EditorInput;
using System.Configuration;

[assembly: CommandClass(typeof(AUtocadWPFFrozLayer04_12_2023.ClassFrozen))]

namespace AUtocadWPFFrozLayer04_12_2023
{
    // программа сделана на основе _____________    autocad_wf_layer_add_29-03-2023
    public class ClassFrozen
    {
        string connect = ConfigurationSettings.AppSettings["MyClasses.ConnectionString"];
        // поле для хранения списка

        public List<string> ClTransports { get; set; }
        public ClassFrozen() { ClTransports = new List<string>() { }; }

        [CommandMethod("FrozenLayers")]
        public void NewCommand()
        {
            // проверка по дате использования
            CheckDate();
            // запускаем окно
            UserControl1 userControl1 = new UserControl1(this);

            AcadApp.Application.ShowModalWindow(userControl1);

            // получаем текущий документ и его БД
            AcadApp.Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;
            // блок кода для вывода сообщения в консоль
            //DocumentManager.MdiActiveDocument.Editor;
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //ed.Editor acEd = acDoc.Editor;
            //ed = acDoc.Editor;
            //ed.WriteMessage("Hi!"); //Вывести сообщение в консоль

            // переменная стринг для работы со слоями
            //string froz = "0";
            //List<string> strings = new List<string>() { "1", "2" };
            // блокируем документ
            using (AcadApp.DocumentLock docloc = acDoc.LockDocument())
            {
                // начинаем транзакцию
                using (Transaction tr = acCurDb.TransactionManager.StartTransaction())
                {
                    // открываем таблицу слоев документа
                    LayerTable acLyrTbl = tr.GetObject(acCurDb.LayerTableId, OpenMode.ForWrite) as LayerTable;
                    // счетчик для количества отсутствующих слоёв
                    int count = 0;
                    try
                    {
                        foreach (var froz in ClTransports)
                        {
                            bool Flag = false;
                            // если в таблице слоев нет нашего слоя - прекращаем выполнение команды
                            if (acLyrTbl.Has(froz) == false)
                            {
                                // выводим имя слоя которого нет
                                ed.WriteMessage($"нет такого слоя - {froz}\n"); //Вывести сообщение в консоль
                                // переходим по метке, чтобы слой не замораживать. 
                                // если слоя не существует - будет ошибка
                                count++;
                                Flag = true;
                                goto ErrorOccured;
                                // стираем список
                                //    ClTransports = new List<string>();
                                //return;
                            }

                            // получаем запись слоя для изменения
                            LayerTableRecord acLyrTblRec = tr.GetObject(acLyrTbl[froz], OpenMode.ForWrite) as LayerTableRecord;

                            // скрываем и блокируем слой
                            //acLyrTblRec.Name = "test";
                            //acLyrTblRec.IsOff = true;
                            // замораживаем слой
                            acLyrTblRec.IsFrozen = true;
                        //acLyrTblRec.IsLocked = true;
                        // Переход по условию отсутствия слоя
                        ErrorOccured: 
                            ed.WriteMessage($"Замораживаем  {froz}  слой\n"); //Вывести сообщение в консоль
                            //MessageBox.Show($"нет {count}  слоёв");
                        }
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show($"Проверте текущий слой  - {ex.Message}");
                    }
                    finally
                    {
                        // стираем список
                        ClTransports = new List<string>() { };
                        ed.WriteMessage($"заморожено  {count}  слоёв\n"); //Вывести сообщение в консоль
                    }
                    // фиксируем транзакцию
                    tr.Commit();
                    // стираем список
                    ClTransports = new List<string>() { };
                }
            }
        }
        // проверка по дате использования
        public static void CheckDate()
        {
            DateTime dt1 = DateTime.Now;
            DateTime dt2 = DateTime.Parse("01/01/2024");
            Window w1 = new Window();

            if (dt1.Date > dt2.Date)
            {
                //MessageBox.Show("Your Application is Expire");
                // Выход из проложения добавил 12-07-2023. Чтобы порядок был....
                Application.Current.Shutdown();
                //w1.Close();
            }
            else
            {
               // MessageBox.Show("Работайте до   " + dt2.ToString());
            }
        }

    }
}
