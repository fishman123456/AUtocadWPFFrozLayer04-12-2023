using AcadApp = Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUtocadWPFFrozLayer04_12_2023
{
    // программа сделана на основе _____________    autocad_wf_layer_add_29-03-2023
    public class ClassFrozen
    {
        // поле для хранения списка
        public List<string> ClTransports = new List<string>() { "1","2" };

        [CommandMethod("NewCommand")]
        public void NewCommand()
        {
            // запускаем окно
            UserControl1 userControl1 = new UserControl1();
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

                    foreach (var froz in ClTransports)
                    {
                        // если в таблице слоев нет нашего слоя - прекращаем выполнение команды
                        if (acLyrTbl.Has(froz) == false)
                        {
                            return;
                        }

                        // получаем запись слоя для изменения
                        LayerTableRecord acLyrTblRec = tr.GetObject(acLyrTbl[froz], OpenMode.ForWrite) as LayerTableRecord;

                        // скрываем и блокируем слой
                        //acLyrTblRec.Name = "test";
                        acLyrTblRec.IsOff = true;
                        // замораживаем слой
                        acLyrTblRec.IsFrozen = true;
                        //acLyrTblRec.IsLocked = true;
                    }
                    // фиксируем транзакцию
                    tr.Commit();
                    
                }
            }
        }
        public List<string> ListTransport(List<string> transports)
        {
            foreach (var transport in transports)
            {
                ClTransports.Add(transport);
            }
            return ClTransports;
        }
    }
}
