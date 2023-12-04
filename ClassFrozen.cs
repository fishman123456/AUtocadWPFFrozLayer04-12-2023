using AcadApp = Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AUtocadWPFFrozLayer04_12_2023
{
    // программа сделана на основе _____________    autocad_wf_layer_add_29-03-2023
    public class ClassFrozen
    {
        // поле для хранения списка
       
        public  List<string> ClTransports { get; set; }
        public ClassFrozen() { ClTransports = new List<string>() { }; }

        [CommandMethod("NewCommand")]
        public void NewCommand()
        {
            // запускаем окно
            UserControl1 userControl1 = new UserControl1(this);
            AcadApp.Application.ShowModalWindow(userControl1);

            // получаем текущий документ и его БД
            AcadApp.Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;
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
                    try
                    {

                    
                    foreach (var froz in ClTransports)
                    {
                        // если в таблице слоев нет нашего слоя - прекращаем выполнение команды
                        if (acLyrTbl.Has(froz) == false)
                        {
                            // выводим имя слоя которого нет
                            MessageBox.Show($"нет такого слоя - {froz}"); 
                            // стираем список
                            ClTransports = new List<string>() { };
                            return;
                        }

                        // получаем запись слоя для изменения
                        LayerTableRecord acLyrTblRec = tr.GetObject(acLyrTbl[froz], OpenMode.ForWrite) as LayerTableRecord;

                        // скрываем и блокируем слой
                        //acLyrTblRec.Name = "test";
                        //acLyrTblRec.IsOff = true;
                        // замораживаем слой
                        acLyrTblRec.IsFrozen = true;
                        //acLyrTblRec.IsLocked = true;
                    }
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show($"что-то не так - {ex.Message}");
                    }
                    finally
                    {
                        // стираем список
                        ClTransports = new List<string>() { };
                    }
                    // фиксируем транзакцию
                    tr.Commit();
                    // стираем список
                    ClTransports = new List<string>() { };
                }
            }
        }
       
    }
}
