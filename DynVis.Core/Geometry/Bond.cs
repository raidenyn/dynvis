using System.Collections;
using System.Collections.Generic;
using DynVis.Core.Elements;

namespace DynVis.Core.Geometry
{
    /// <summary>
    /// Класс работы со связью между атомами
    /// </summary>
    public class Bond: IBond
    {
        public Bond(IAtom atom1, IAtom atom2, BondType bondType)
        {
            Atom1 = atom1;
            Atom2 = atom2;

            BondType = bondType;
        }

        public Bond(IBond bond)
            : this(bond.Atom1, bond.Atom2, bond.BondType)
        {
        }

        
        public IAtom Atom1
        {
            get;
            set;
        }

        public IAtom Atom2
        {
            get;
            set;
        }

        public BondType BondType
        {
            get;
            set;
        }
    }

    /// <summary>
    /// Список связей в структуре
    /// </summary>
    public class BondList : IBondList
    {
        private readonly List<IBond> BondsList = new List<IBond>();

        /// <summary>
        /// Определяет возможность присутсвия связи с типом None в списке
        /// Иначе запись просто удаляется
        /// </summary>
        public bool AllowNoneBond;

        #region Collection Function

        public IEnumerator<IBond> GetEnumerator()
        {
            return BondsList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IBond this[int index]
        {
            get { return BondsList[index]; }
            set { BondsList[index] = value; }
        }

        public void Add(IBond item)
        {
            var oldBond = GetBond(item.Atom1, item.Atom2);
            if (oldBond == null)
            {
                if (item.BondType != BondType.None || AllowNoneBond)
                {
                    BondsList.Add(item);
                }
            }
            else
            {
                if (item.BondType != BondType.None || AllowNoneBond)
                {
                    BondsList.Insert(BondsList.IndexOf(oldBond), item);
                }
                BondsList.Remove(oldBond);
            }

        }

        public void Clear()
        {
            BondsList.Clear();
        }

        public bool Contains(IBond item)
        {
            return BondsList.Contains(item);
        }

        public void CopyTo(IBond[] array, int arrayIndex)
        {
            BondsList.CopyTo(array, arrayIndex);
        }

        public bool Remove(IBond item)
        {
            return BondsList.Remove(item);
        }

        public int Count
        {
            get { return BondsList.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        #endregion

        /// <summary>
        /// Возращает связь между двумя атомами
        /// </summary>
        /// <param name="atom1"></param>
        /// <param name="atom2"></param>
        /// <returns></returns>
        public IBond GetBond(IAtom atom1, IAtom atom2)
        {
            return GetBond(atom1, atom2, BondsList);
        }

        #region Static Function

        /// <summary>
        /// Определяет тип связи между двумя атомами
        /// </summary>
        /// <param name="atom1">Первый атом</param>
        /// <param name="atom2">Второй атом</param>
        /// <param name="bondList">Список связей</param>
        /// <returns>Тип связи между атомами</returns>
        public static IBond GetBond(IAtom atom1, IAtom atom2, ICollection<IBond> bondList)
        {
            foreach (var bond in bondList)
            {
                if ((bond.Atom1 == atom1 && bond.Atom2 == atom2) ||
                    (bond.Atom1 == atom2 && bond.Atom2 == atom1)) return bond;
            }

            return null;
        }

        /// <summary>
        /// Объединяет два списка связей с приоретеом последнего
        /// </summary>
        /// <param name="nativeBondList"></param>
        /// <param name="manualBondList"></param>
        /// <returns></returns>
        public static ICollection<Bond> JoinVisibleBonds(ICollection<IBond> nativeBondList, ICollection<IBond> manualBondList)
        {
            var ResultList = new List<Bond>();

            foreach (var nativeBond in nativeBondList)
            {
                if (nativeBond.BondType != BondType.None) ResultList.Add(new Bond(nativeBond));
            }
            
            foreach (var manualBond in manualBondList)
            {
                var bond = (Bond)GetBond(manualBond.Atom1, manualBond.Atom2, ResultList.ToArray());
                if (bond == null)
                {
                    if (manualBond.BondType != BondType.None) ResultList.Add(new Bond(manualBond));
                } 
                else
                {
                    if (manualBond.BondType == BondType.None)
                    {
                        ResultList.Remove(bond);
                    } 
                    else
                    {
                        bond.BondType = manualBond.BondType;
                    }
                }
            }

            return ResultList;
        }

        /// <summary>
        /// Функция обновляет список связей на основе ковалентных радиусов
        /// </summary>
        /// <param name="atomStructure">Атомная структура</param>
        /// <param name="bondList">Список связей</param>
        public static void UpdateBondsByCovalentRadius(IAtomStructure atomStructure, BondList bondList)
        {
            bondList.Clear();
            for (var a1=1; a1 < atomStructure.Count; a1++)
            {
                for (var a2 = 0; a2 < a1; a2++)
                {
                    var atom1 = atomStructure[a1];
                    var atom2 = atomStructure[a2];

                    var bondType = atom1.IsCovalentRadiusBond(atom2);
                    if (bondType != BondType.None)
                    {
                        bondList.Add(new Bond(atom1, atom2, bondType));
                    }
                }
            }
        }

        #endregion
    }
}
