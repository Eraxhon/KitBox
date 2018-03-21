using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Test_kitbox
{
    static class Catalog
    {
        private static List<Piece> pieceList = new List<Piece>();

        public static List<String> GetPieces()
        {
            List<String> pieces = new List<String>();
            using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=C:\Users\15171\Desktop\Kitbox.db;Version=3;"))
            {
                connect.Open();
                using (SQLiteCommand fmd = connect.CreateCommand())
                {
                    fmd.CommandText = @"SELECT * FROM Piece INNER JOIN Reference ON Piece.ID_Piece = Reference.ID_Piece";
                    SQLiteDataReader q = fmd.ExecuteReader();

                    while (q.Read())
                    {
                        string reference = Convert.ToString(q["Reference"]);

                        if (reference == "Angle bar")
                        {
                            int height = Convert.ToInt16(q["Height"]);
                            string color = Convert.ToString(q["Color"]);
                            int price = Convert.ToInt16(q["Price"]);
                            string id = Convert.ToString(q["Piece_Code"]);
                            Piece AngleBar = new AngleBar(height, color, price, id);
                            pieceList.Add(AngleBar);
                        }

                        else if (reference == "B_Panel")
                        {
                            int length = Convert.ToInt16(q["Length"]);
                            int height = Convert.ToInt16(q["height"]);
                            int depth = Convert.ToInt16(q["Depth"]);
                            string color = Convert.ToString(q["Color"]);
                            string type = "Panel back";                                             
                            int price = Convert.ToInt16(q["Price"]);
                            string id = Convert.ToString(q["Piece_ID"]);
                            Piece Panel = new Panel(length, height, depth, color, type, price, id);
                            pieceList.Add(Panel);
                        }

                        else if (reference == "LR_Panel")
                        {
                            int length = Convert.ToInt16(q["Length"]);
                            int height = Convert.ToInt16(q["height"]);
                            int depth = Convert.ToInt16(q["Depth"]);
                            string color = Convert.ToString(q["Color"]);
                            string type = "Panel left right";                                             
                            int price = Convert.ToInt16(q["Price"]);
                            string id = Convert.ToString(q["Piece_ID"]);
                            Piece Panel = new Panel(length, height, depth, color, type, price, id);
                            pieceList.Add(Panel);
                        }

                        else if (reference == "TB_Panel")
                        {
                            int length = Convert.ToInt16(q["Length"]);
                            int height = Convert.ToInt16(q["height"]);
                            int depth = Convert.ToInt16(q["Depth"]);
                            string color = Convert.ToString(q["Color"]);
                            string type = "Panel top bottom";                                             
                            int price = Convert.ToInt16(q["Price"]);
                            string id = Convert.ToString(q["Piece_ID"]);
                            Piece Panel = new Panel(length, height, depth, color, type, price, id);
                            pieceList.Add(Panel);
                        }

                        else if (reference == "Door")
                        {
                            int length = Convert.ToInt16(q["Length"]);
                            int height = Convert.ToInt16(q["height"]);
                            string color = Convert.ToString(q["Color"]);
                            int price = Convert.ToInt16(q["Price"]);
                            string id = Convert.ToString(q["Piece_ID"]);
                            Piece Door = new Door(length, height, color, price, id);
                            pieceList.Add(Door);
                        }

                        else if (reference == "Cleat")
                        {
                            int height = Convert.ToInt16(q["height"]);
                            int price = Convert.ToInt16(q["Price"]);
                            string id = Convert.ToString(q["Piece_ID"]);
                            Piece Cleat = new Cleat(height, price, id);
                            pieceList.Add(Cleat);
                        }

                        else if (reference == "B_Rail")
                        {
                            string type = "Rail back"; 
                            int length = Convert.ToInt16(q["Length"]);
                            int price = Convert.ToInt16(q["Price"]);
                            string id = Convert.ToString(q["Piece_ID"]);
                            Piece Rail = new Rail(type, length, price, id);
                            pieceList.Add(Rail);
                        }

                        else if (reference == "F_Rail")
                        {
                            string type = "Rail front";
                            int length = Convert.ToInt16(q["Length"]);
                            int price = Convert.ToInt16(q["Price"]);
                            string id = Convert.ToString(q["Piece_ID"]);
                            Piece Rail = new Rail(type, length, price, id);
                            pieceList.Add(Rail);
                        }

                        else if (reference == "LR_Rail")
                        {
                            string type = "Rail left right";
                            int length = Convert.ToInt16(q["Length"]);
                            int price = Convert.ToInt16(q["Price"]);
                            string id = Convert.ToString(q["Piece_ID"]);
                            Piece Rail = new Rail(type, length, price, id);
                            pieceList.Add(Rail);
                        }

                        else if (reference == "Knob")
                        {
                            int diameter = Convert.ToInt16(q["Dimensions"]); //ATTENTION changer le diamètre dans la table par juste le chiffre
                            int price = Convert.ToInt16(q["Price"]);
                            string id = Convert.ToString(q["Piece_ID"]);
                            Piece Knob = new Knob(diameter, price, id);
                            pieceList.Add(Knob);
                        }
                    }
                }

            }

            return pieces;
        }

        public static List<Piece> PieceList
        {
            get
            {
                List<Piece> newList = new List<Piece>();

                foreach (Piece i in pieceList)
                {
                    newList.Add(i);
                }

                return newList;
            }
        }

        public static void AddPiece(Piece piece)
        {
            pieceList.Add(piece);
        }

        //UpdateFromDB

        public static Piece FindCleat(int height)
        {
            Cleat cleat;
            foreach (Piece piece in pieceList)
            {
                if (piece is Cleat)
                {
                    cleat = piece as Cleat;
                    if (cleat.Height == height)
                    {
                        return cleat;
                    }
                }
            }

            return null;
        }

        public static Piece FindAngleBar(int height, string color)
        {
            AngleBar angleBar, shortestAngleBar = null;

            foreach (Piece piece in pieceList)
            {
                if (piece is AngleBar)
                {
                    angleBar = piece as AngleBar;
                    if (angleBar.Height >= height && angleBar.Color == color)
                    {
                        if(shortestAngleBar.Height > angleBar.Height)
                        {
                            shortestAngleBar = angleBar;
                        }
                    }
                }
            }

            return shortestAngleBar;
        }

        public static Piece FindRail(string type, int length)
        {
            Rail rail;
            foreach (Piece piece in pieceList)
            {
                if (piece is Rail)
                {
                    rail = piece as Rail;
                    if (rail.Type == type && rail.Length == length)
                    {
                        return rail;
                    }
                }
            }

            return null;
        }

        public static Piece FindDoor(int length, int height, string color)
        {
            Door door;
            foreach (Piece piece in pieceList)
            {
                if (piece is Rail)
                {
                    door = piece as Door;
                    if (door.Length == length && door.Height == height && door.Color == color)
                    {
                        return door;
                    }
                }
            }

            return null;
        }

        public static Piece FindPanel(int length, int height, int depth, string color, string type)
        {
            Panel panel;
            foreach (Piece piece in pieceList)
            {
                if (piece is Rail)
                {
                    panel = piece as Panel;
                    if (panel.Type == type && panel.Length == length && panel.Height == height 
                        && panel.Depth == depth && panel.Color == color)
                    {
                        return panel;
                    }
                }
            }

            return null;
        }

        public static Piece FindKnob(int diameter)
        {
            Knob knob;
            foreach (Piece piece in pieceList)
            {
                if (piece is Knob)
                {
                    knob = piece as Knob;
                    if (knob.Diameter == diameter)
                    {
                        return knob;
                    }
                }
            }

            return null;
        }

    }
}
