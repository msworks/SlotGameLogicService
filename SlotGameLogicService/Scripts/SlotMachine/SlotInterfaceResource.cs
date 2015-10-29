using System;

public class SlotInterfaceResource
{
	public static sbyte[] getResourceData(string strPath) 
	{
        sbyte[] loadBytes = null;
        loadBytes = SaveData.LoadChipData();

		return loadBytes;
	}
}
