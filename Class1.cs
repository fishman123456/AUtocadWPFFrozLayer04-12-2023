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
    public class Class1
    {
        [CommandMethod("NewCommand")]
        public void NewCommand(string[] strings)
        {
            // получаем текущий документ и его БД
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;

            // блокируем документ
            using (DocumentLock docloc = acDoc.LockDocument())
            {
                // начинаем транзакцию
                using (Transaction tr = acCurDb.TransactionManager.StartTransaction())
                {
                    // открываем таблицу слоев документа
                    LayerTable acLyrTbl = tr.GetObject(acCurDb.LayerTableId, OpenMode.ForWrite) as LayerTable;

                    // если в таблице слоев нет нашего слоя - прекращаем выполнение команды
                    if (acLyrTbl.Has("HabrLayer") == false)
                    {
                        return;
                    }

                    // получаем запись слоя для изменения
                    LayerTableRecord acLyrTblRec = tr.GetObject(acLyrTbl["HabrLayer"], OpenMode.ForWrite) as LayerTableRecord;

                    // скрываем и блокируем слой
                    acLyrTblRec.Name = "test";
                    acLyrTblRec.IsOff = true;
                    acLyrTblRec.IsLocked = true;

                    // фиксируем транзакцию
                    tr.Commit();
                }
            }
        }
    }
}
