﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AtomConfig
{
    public class Bonds
    {
        public float ENTreshold = 1.8f;
        public Dictionary<KeyValuePair<Atom, Atom>, int> data = new Dictionary<KeyValuePair<Atom, Atom>, int>();
        public Bonds() {

        }

        public void RemoveBonds(Atom A)
        {
            A.ResetState();
            //maybe use .maping next time
            //parameterList.Remove(parameterList.Where(k => String.Compare(k.Key, "someKeyName") == 0)); 
            foreach (KeyValuePair<Atom, Atom> kvp in data.Keys)
            {
                if (kvp.Key == A || kvp.Value == A)
                {
                    data.Remove(kvp);
                }
            }
            //remove all entries with A in it from data
        }

        public void ElectronTrading(Atom A, Atom B, string type)
        {
            if (type == "ionic")
            {
                if (A.config.EN > B.config.EN)
                {
                    A.shared++; //isn't sharing technically but for now no need to make extra variable "stolen"
                    B.lost++;
                }
                else
                {
                    A.lost++;
                    B.shared++;
                }
            }
            else if (type == "covalent")
            {
                //will be an issue with visualization --> not actually more electrons in scene
                // ___solved___ .config remains immutable
                A.shared++;
                B.shared++;
            }
        }

        public string BondType(Atom A, Atom B)
        {
            float deltaEN = Mathf.Abs(A.config.EN - B.config.EN);
            if (deltaEN > ENTreshold)
            {
                return "ionic";
            }
            return "covalent";
        }

        //if bond is ionic => steal else share

        public void AddBond(KeyValuePair<Atom, Atom> bond)
        {
            bool isThere = data.ContainsKey(bond);
            KeyValuePair<Atom, Atom> reverse = new KeyValuePair<Atom, Atom>(bond.Value, bond.Key);
            bool isThereReverse = data.ContainsKey(reverse);

            if (isThere && isThereReverse)
            //merge with the other bond --> direction doesn't count
            //maybe let normal and reversed coexist?? someday
            {
                int otherStrength = data[reverse];
                data[bond] += otherStrength;
                data.Remove(reverse);
            }
            else if (isThere && !isThereReverse)
            {
                data[bond]++;
            }
            else if (!isThere && isThereReverse)
            {
                data[reverse]++;
            }
            else if (!isThere && !isThereReverse)
            {
                data.Add(bond, 1);
            }
        }
        public bool isValidBond(Atom A, Atom B)
        {
            bool isEmpty = (A == null || B == null);
            bool isSame = (A == B);
            bool canConnect = !A.isFull() && !B.isFull();

            return !isEmpty && !isSame && canConnect;
        }
    }

    public class Config {
        public string symbol;
        public string name;
        public Color color;
        public float size;
        public int protons;
        public int valence;
        public int capacity;
        public float EN; //ElectroNegativity
        public Atom[] bonded;
        public int valenceOrbit;
    }
    public static class PTable
    {
        public static int[] OrbitCapacity = new int[] { 2, 8, 18 };
        public static List<Config> config = new List<Config>(){

            new Config()
            {
                name = "noAtom"
            },
             new Config(){
                 name = "Hydrogen",
                 color = Color.white,
                 size = 1f,
                 symbol = "H",
                 protons = 1,
                 valence = 1,
                 EN = 2.2f,
                 capacity = 2,
                 valenceOrbit = 1
             },
             new Config(){
                 name = "Helium",
                 color = Color.blue,
                 size = 1.2f,
                 symbol = "H",
                 protons = 2,
                 valence = 2,
                 capacity = 2,
                 EN = 3.44f,
                 valenceOrbit = 1,
             },
             new Config(){
                 name = "Lithium",
                 color = Color.red,
                 size = 1.4f,
                 symbol = "Li",
                 protons = 3,
                 valence = 1,
                 capacity = 8,
                 EN = 3.44f,
                 valenceOrbit = 2,
             },
             new Config(){
                 name = "Berylium",
                 symbol = "Be",
                 color = Color.red,
                 size = 1.45f,
                 protons = 4,
                 valence = 2,
                 capacity = 8,
                 EN = 3.44f,
                 valenceOrbit = 2,
             },
             new Config(){
                 name = "Boron",
                 color = Color.blue,
                 size = 1.2f,
                 symbol = "B",
                 protons = 5,
                 valence = 3,
                 capacity = 8,
                 EN = 3.44f,
                 valenceOrbit = 2,
             },
             new Config(){
                 name = "Carbon",
                 color = Color.grey,
                 size = 1.6f,
                 symbol = "C",
                 protons = 6,
                 valence = 4,
                 capacity = 8,
                 EN = 3.44f,
                 valenceOrbit = 2,
             },
             new Config(){
                 name = "Nitrogen",
                 color = Color.green,
                 size = 1.7f,
                 symbol = "N",
                 protons = 7,
                 valence = 5,
                 capacity = 8,
                 EN = 3.44f,
                 valenceOrbit = 2,
             },
             new Config(){
                 name = "Oxygen",
                 symbol = "O",
                 color = Color.blue,
                 size = 1.9f,
                 protons = 8,
                 valence = 6,
                 capacity = 8,
                 EN = 3.44f,
                 valenceOrbit = 2,
             },
             new Config(){
                 name = "Fluorine",
                 symbol = "F",
                 protons = 9,
                 valence = 7,
                 capacity = 8,
                 EN = 3.44f,
                 valenceOrbit = 2,
             }
        };
    }
}


