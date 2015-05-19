using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Duality.Records
{
    public class ScoreBoard
    {
        //This class is designed to handle the high score menu and how to record new high scores
        //This class also encrypts high scores so the file cannot be tampered with.
        String[] textHighScores1 = new string[10];
        String[] textHighScores2 = new string[10];
        String[] encryptedHighScores1 = new string[10];
        String[] encryptedHighScores2 = new string[10];
        String[] textNames1 = new string[10];
        String[] textNames2 = new string[10];
        String[] encryptedNames1 = new string[10];
        String[] encryptedNames2 = new string[10];
        int numOneScore = 0, dummy;
        string fileName, rootElement;
        Boolean boolWorkingFileIO = true;
        XmlDocument scoresWrite, scoresRead;

        public int getNumOneScore() { return numOneScore; }

        public ScoreBoard(string fileName, string rootElement)
        {
            this.fileName = fileName;
            this.rootElement = rootElement;
        }

        public void retrieveScores()
        {
            boolWorkingFileIO = true;

            try
            {
                scoresRead = new XmlDocument();
                scoresRead.Load(fileName + ".xml");

                for (int i = 0; i < 10; i++)
                {
                    encryptedHighScores1[i] = scoresRead.SelectSingleNode("/" + rootElement + "/Score" + (i + 1)).Attributes["Score"].Value;
                    encryptedNames1[i] = scoresRead.SelectSingleNode("/" + rootElement + "/Score" + (i + 1)).Attributes["Name"].Value;

                    if (encryptedHighScores1[i] != "")
                    {
                        textHighScores1[i] = Encrypting.StringCipher.Decrypt(encryptedHighScores1[i], "G5bM(1inE|`JT(@GX5?:O=<*t<_EgB");
                    }
                    else
                    {
                        textHighScores1[i] = "0";
                    }

                    if (encryptedNames1[i] != "")
                    {
                        textNames1[i] = Encrypting.StringCipher.Decrypt(encryptedNames1[i], "US1qeI3{s%XfLP911(zckW3T)-C70F");
                    }
                    else
                    {
                        textNames1[i] = "AAA";
                    }

                    if (textHighScores1[i] == "")
                    {
                        textHighScores1[i] = "0";
                    }
                }
                scoresRead.Save("tetroHighScores.xml");
                numOneScore = Convert.ToInt32(textHighScores1[0]);
            }
            catch
            {
                Console.WriteLine("No high score file was found");
                Console.WriteLine("Generating tetroHighScores.xml");
                boolWorkingFileIO = false;
                XmlDocument create = new XmlDocument();
                XmlNode rootNode = create.CreateElement("TetroScores");
                create.AppendChild(rootNode);

                for (int i = 1; i < 11; i++)
                {
                    XmlNode userNode = create.CreateElement("Score" + i);
                    XmlAttribute attribute = create.CreateAttribute("Score");
                    attribute.Value = "";
                    userNode.Attributes.Append(attribute);
                    XmlAttribute nameAttribute = create.CreateAttribute("Name");
                    nameAttribute.Value = "";
                    userNode.Attributes.Append(nameAttribute);
                    rootNode.AppendChild(userNode);
                }
                create.Save("tetroHighScores.xml");
            }
        }

        //Records Scores to an XML file on hard drive
        public void recordScore(int sc, string n)
        {
            boolWorkingFileIO = true;

            retrieveScores();

            //If there are no file problems continue
            if (boolWorkingFileIO)
            {
                for (int i = 0, j = 0; i < 10; i++, j++)
                {
                    if (sc > Convert.ToInt32(textHighScores1[i]) && i == j)
                    {
                        textHighScores2[i] = sc.ToString();
                        textNames2[i] = n.ToString();
                        i++;
                        if (i < 10)
                        {
                            textHighScores2[i] = textHighScores1[j];
                            textNames2[i] = textNames1[j];
                        }
                    }
                    else
                    {
                        textHighScores2[i] = textHighScores1[j];
                        textNames2[i] = textNames1[j];
                    }
                }
            }

            //Write the new scores to the file
            try
            {
                dummy = 1;

                while (dummy == 1)
                {
                    scoresWrite = new XmlDocument();

                    scoresWrite.Load("tetroHighScores.xml");

                    for (int i = 0; i < 10; i++)
                    {
                        encryptedHighScores2[i] = Encrypting.StringCipher.Encrypt(textHighScores2[i], "G5bM(1inE|`JT(@GX5?:O=<*t<_EgB");
                        encryptedNames2[i] = Encrypting.StringCipher.Encrypt(textNames2[i], "US1qeI3{s%XfLP911(zckW3T)-C70F");
                        scoresWrite.SelectSingleNode("/TetroScores/Score" + (i + 1)).Attributes["Score"].Value = encryptedHighScores2[i];
                        scoresWrite.SelectSingleNode("/TetroScores/Score" + (i + 1)).Attributes["Name"].Value = encryptedNames2[i];
                    }
                    scoresWrite.Save("tetroHighScores.xml");
                    dummy = 0;
                }
            }
            catch
            {
                Console.WriteLine("No high score file was found");
                Console.WriteLine("Generating tetroHighScores.xml");
                boolWorkingFileIO = false;
                XmlDocument create = new XmlDocument();
                XmlNode rootNode = create.CreateElement("TetroScores");
                create.AppendChild(rootNode);

                for (int i = 1; i < 11; i++)
                {
                    XmlNode userNode = create.CreateElement("Score" + i);
                    XmlAttribute attribute = create.CreateAttribute("Score");
                    attribute.Value = "";
                    userNode.Attributes.Append(attribute);
                    XmlAttribute nameAttribute = create.CreateAttribute("Name");
                    nameAttribute.Value = "";
                    userNode.Attributes.Append(nameAttribute);
                    rootNode.AppendChild(userNode);
                }
                create.Save("tetroHighScores.xml");
            }
        }
    }
}

