﻿/**
 * Mascot3D.java
 *
 * Created on 2007/08/20
 */

public class Mascot3D
{
    ZZ ZZ;

    public Mascot3D(ZZ ZZ)
    {
        this.ZZ = ZZ;
    }

    /** 3D angle */
    const int angle_x = 0;

    /** 3D scale */
    const int scale = 100 / 2;

    /***/
    int act_frame = 0;
    /***/
    int act_frame_d = 65535;

    /** 4th Reel Texture */
    int[] tex01_on = {
		Defines.DEF_RES_TEX01_KOUKA,
		Defines.DEF_RES_TEX02_4TH_L,
		Defines.DEF_RES_TEX03_4TH_R,
		Defines.DEF_RES_TEX05_KOUKA2,
	};

    /** 4th Reel Texture */
    int[] tex01_off = {
		Defines.DEF_RES_TEX01_KOUKA,
		Defines.DEF_RES_TEX02_4TH_L2,
		Defines.DEF_RES_TEX03_4TH_R2,
		Defines.DEF_RES_TEX05_KOUKA,
	};

    public void initModel()
    {
        ZZ.setTextures(Defines.DEF_RES_MBAC, tex01_on);
        ZZ.setPosture(Defines.DEF_RES_MBAC, Defines.DEF_RES_MTRA, 0, 0);
    }

    /**
     * setTexture: 4th Reel
     *
     * @param type true==setBlurTexture
     */
    public void setTexture(int type)
    {
        if (type == 1)
        {
            ZZ.setTextures(Defines.DEF_RES_MBAC, tex01_on);
        }
        else
        {
            ZZ.setTextures(Defines.DEF_RES_MBAC, tex01_off);
        }
    }

    /***/
    public void draw3Dtest(int frame)
    {
        //-- カメラ設定 --
        ZZ.setViewTrans(
            0, 0, -4096, //position
            0, 0, 1, //look
            0, -1, 0 //up
        );
        ZZ.scale3D(scale);

        ZZ.setClip(61, 15 + Defines.DEF_RES_MTRA + Defines.GP_DRAW_OFFSET_Y, 118, 53);//TODO 実際のデータを..
        ZZ.drawFigure(Defines.DEF_RES_MBAC);
        ZZ.setClip(0, 0, 240, 240);//TODO 実際のデータを..

        ZZ.flush3D();

        if (false)
        {
            //フレームを進める.
            ZZ.incPostureFrame(Defines.DEF_RES_MTRA, 0, 0);
        }
        else
        {
            int len = ZZ.getActionFrameLength(Defines.DEF_RES_MTRA, 0);
            //フレームを任意に進める.
            ZZ.setPosture(Defines.DEF_RES_MBAC, Defines.DEF_RES_MTRA, 0, frame);
        }
    }


}
