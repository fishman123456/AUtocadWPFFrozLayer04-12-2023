using Autodesk.AutoCAD.ApplicationServices;
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
        [CommandMethod("NewCommand")]
        public void NewCommand()
        {
            // получаем текущий документ и его БД
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;
            // переменная стринг для работы со слоями
            //string froz = "0";
            List<string> strings = new List<string>() { "1", "2" };
            // блокируем документ
            using (DocumentLock docloc = acDoc.LockDocument())
            {
                // начинаем транзакцию
                using (Transaction tr = acCurDb.TransactionManager.StartTransaction())
                {
                    // открываем таблицу слоев документа
                    LayerTable acLyrTbl = tr.GetObject(acCurDb.LayerTableId, OpenMode.ForWrite) as LayerTable;

                    foreach (var froz in strings)
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
    }
}
