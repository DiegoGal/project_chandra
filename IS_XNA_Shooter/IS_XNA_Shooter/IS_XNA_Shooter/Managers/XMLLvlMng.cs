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
        public static XmlDocument lvl1A;     // XML level 1 A
        public static XmlDocument lvl1B;     // XML level 1 B
        public static XmlDocument lvl1C;     // XML level 1 C
        public static XmlDocument rect1;    // rectangles level 1 side scroll mode
        public static XmlDocument rect2;    // rectangles level 2 side scroll mode (DORITO)
        public static XmlDocument dialog1;  // dialog of the first level of mode history

        /// <summary>
        /// Constructor for XMLLvl manager
        /// </summary>
        public XMLLvlMng()
        {
            // TODO: Complete member initialization
        }

        /// <summary>
        /// Load the content of the game indicated by the param i
        /// </summary>
        /// <param name="i">indicator for the content loaded</param>
        public void LoadContent(int i)
        {
            // i:
            // 0=GameA
            // 1=GameB

            switch (i)
            {
                case 0: // GameA
                    lvl1A = new XmlDocument();
                    lvl1A.Load("../../../../IS_XNA_ShooterContent/Levels/level1A.xml");
                    break;
                case 1: // GameB Level 1
                    rect1 = new XmlDocument();
                    rect1.Load("../../../../IS_XNA_ShooterContent/Levels/levelRectangle1.xml");
                    lvl1C = new XmlDocument();
                    lvl1C.Load("../../../../IS_XNA_ShooterContent/Levels/level1B.xml");
                    break;

                case 2: // dialogs
                    dialog1 = new XmlDocument();
                    dialog1.Load("../../../../IS_XNA_ShooterContent/Levels/dialog1.xml");
                    break;

                case 3: // GameB Level 2 DORITO
                    rect2 = new XmlDocument();
                    rect2.Load("../../../../IS_XNA_ShooterContent/Levels/levelRectangle2.xml");
                    break;
            }

        } // LoadContent

       /// <summary>
       /// Unload the content of the XML level manager
       /// </summary>
       /// <param name="i">indicates the resources to unload</param>
        public void UnloadContent(int i)
        {
            //i:
            // 0=GameA
            // 1=GameB
            switch (i)
            {
                case 0: //gameA
                    lvl1A = null;
                    break;
                case 1: //gameB
                    lvl1C = null;
                    rect1 = null;
                    dialog1 = null;
                    break;
            }
        }
       

    } // class XMLLvlMng
}
