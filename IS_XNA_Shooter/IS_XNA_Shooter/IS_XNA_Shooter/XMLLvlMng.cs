﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Xml.Linq;
using System.Xml;
using System.IO;

namespace IS_XNA_Shooter
{
    class XMLLvlMng // XML Level Manager
    {
        public static XmlDocument lvl1;     // XML level 1
        public static XmlDocument rect1;    // rectangles level 1 side scroll mode

        public XMLLvlMng()
        {
            // TODO: Complete member initialization
        }

        public void LoadContent(int i)
        {
            // i:
            // 0=GameA
            // 1=GameB

            switch (i)
            {
                case 0: // gameA
                    lvl1 = new XmlDocument();
                    lvl1.Load("../../../../IS_XNA_ShooterContent/Levels/level1.xml");
                    break;
                case 1: // gameB
                    rect1 = new XmlDocument();
                    rect1.Load("../../../../IS_XNA_ShooterContent/Levels/levelRectangle1.xml");
                    break;
            }

        } // LoadContent

       //unloadContent
        public void UnloadContent(int i)
        {
            //i:
            // 0=GameA
            // 1=GameB
            switch (i)
            {
                case 0: //gameA
                    lvl1 = null;
                    break;
                case 1: //gameB
                    rect1 = null;
                    break;
            }
        }
       

    } // class XMLLvlMng
}
