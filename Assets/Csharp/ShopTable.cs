//----------------------------------------------
//    这是一个自动生成的脚本                      
//    如非必要请不要直接修改                      
//----------------------------------------------

#pragma warning disable 649

using System;
using UnityEngine;

namespace GameTable {

	public partial class ShopTable : ScriptableObject {

		[NonSerialized]
		private int mVersion = 1;

		[SerializeField]
		public ShopOne[] _ShopOneItems;

		public ShopOne GetShopOne(int itemID) {
			int min = 0;
			int max = _ShopOneItems.Length;
			while (min < max) {
				int index = (min + max) >> 1;
				ShopOne item = _ShopOneItems[index];
				if (item.ItemID == itemID) { return item.Init(mVersion, DataGetterObject); }
				if (itemID < item.ItemID) {
					max = index;
				} else {
					min = index + 1;
				}
			}
			return null;
		}

		[SerializeField]
		public ShopTwo[] _ShopTwoItems;

		public ShopTwo GetShopTwo(int itemID) {
			int min = 0;
			int max = _ShopTwoItems.Length;
			while (min < max) {
				int index = (min + max) >> 1;
				ShopTwo item = _ShopTwoItems[index];
				if (item.ItemID == itemID) { return item.Init(mVersion, DataGetterObject); }
				if (itemID < item.ItemID) {
					max = index;
				} else {
					min = index + 1;
				}
			}
			return null;
		}

		[SerializeField]
		public ShopThree[] _ShopThreeItems;

		public ShopThree GetShopThree(int itemID) {
			int min = 0;
			int max = _ShopThreeItems.Length;
			while (min < max) {
				int index = (min + max) >> 1;
				ShopThree item = _ShopThreeItems[index];
				if (item.ItemID == itemID) { return item.Init(mVersion, DataGetterObject); }
				if (itemID < item.ItemID) {
					max = index;
				} else {
					min = index + 1;
				}
			}
			return null;
		}

		public void Reset() {
			mVersion++;
		}

		public interface IDataGetter {
			ShopOne GetShopOne(int ItemID);
			ShopTwo GetShopTwo(int ItemID);
			ShopThree GetShopThree(int ItemID);
		}

		private class DataGetter : IDataGetter {
			private Func<int, ShopOne> _GetShopOne;
			public ShopOne GetShopOne(int ItemID) {
				return _GetShopOne(ItemID);
			}
			private Func<int, ShopTwo> _GetShopTwo;
			public ShopTwo GetShopTwo(int ItemID) {
				return _GetShopTwo(ItemID);
			}
			private Func<int, ShopThree> _GetShopThree;
			public ShopThree GetShopThree(int ItemID) {
				return _GetShopThree(ItemID);
			}
			public DataGetter(Func<int, ShopOne> getShopOne, Func<int, ShopTwo> getShopTwo, Func<int, ShopThree> getShopThree) {
				_GetShopOne = getShopOne;
				_GetShopTwo = getShopTwo;
				_GetShopThree = getShopThree;
			}
		}

		[NonSerialized]
		private DataGetter mDataGetterObject;
		private DataGetter DataGetterObject {
			get {
				if (mDataGetterObject == null) {
					mDataGetterObject = new DataGetter(GetShopOne, GetShopTwo, GetShopThree);
				}
				return mDataGetterObject;
			}
		}
	}

	[Serializable]
	public class ShopOne {

		[SerializeField]
		private int _ItemID;
		public int ItemID { get { return _ItemID; } }

		[SerializeField]
		private string _ItemName;
		public string ItemName { get { return _ItemName; } }

		[SerializeField]
		private int _ItemCount;
		public int ItemCount { get { return _ItemCount; } }

		[NonSerialized]
		private int mVersion = 0;
		public ShopOne Init(int version, ShopTable.IDataGetter getter) {
			if (mVersion == version) { return this; }
			mVersion = version;
			return this;
		}

		public override string ToString() {
			return string.Format("[ShopOne]{{ItemID:{0}, ItemName:{1}, ItemCount:{2}}}",
				ItemID, ItemName, ItemCount);
		}

	}

	[Serializable]
	public class ShopTwo {

		[SerializeField]
		private int _ItemID;
		public int ItemID { get { return _ItemID; } }

		[SerializeField]
		private string _ItemName;
		public string ItemName { get { return _ItemName; } }

		[SerializeField]
		private int _ItemCount;
		public int ItemCount { get { return _ItemCount; } }

		[NonSerialized]
		private int mVersion = 0;
		public ShopTwo Init(int version, ShopTable.IDataGetter getter) {
			if (mVersion == version) { return this; }
			mVersion = version;
			return this;
		}

		public override string ToString() {
			return string.Format("[ShopTwo]{{ItemID:{0}, ItemName:{1}, ItemCount:{2}}}",
				ItemID, ItemName, ItemCount);
		}

	}

	[Serializable]
	public class ShopThree {

		[SerializeField]
		private int _ItemID;
		public int ItemID { get { return _ItemID; } }

		[SerializeField]
		private string _ItemName;
		public string ItemName { get { return _ItemName; } }

		[SerializeField]
		private int _ItemCount;
		public int ItemCount { get { return _ItemCount; } }

		[NonSerialized]
		private int mVersion = 0;
		public ShopThree Init(int version, ShopTable.IDataGetter getter) {
			if (mVersion == version) { return this; }
			mVersion = version;
			return this;
		}

		public override string ToString() {
			return string.Format("[ShopThree]{{ItemID:{0}, ItemName:{1}, ItemCount:{2}}}",
				ItemID, ItemName, ItemCount);
		}

	}

}
