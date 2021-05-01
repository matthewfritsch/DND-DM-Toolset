using System.Collections;
using System.Collections.Generic;
using System;

public class MonsterParser{

    private string filepath;

    public MonsterParser(string newFilePath){
        filepath = newFilePath;
    }

    public List<MonsterInfo> read(){

        List<MonsterInfo> toRet = new List<MonsterInfo>();
        string[] linesFromFile = System.IO.File.ReadAllLines(filepath);
        foreach(string line in linesFromFile){
            string[] fields = line.Split(',');
            string monsterName = fields[0];
            if(monsterName == "Name") continue;
            Console.WriteLine(monsterName);
            string monsterType = fields[1];
            string monsterAlignment = fields[2];
            Size monsterSize = (Size)Enum.Parse(typeof(Size), fields[3].ToUpper());
            double monsterChallengeRating = Convert.ToDouble(fields[4]);
            short monsterArmorClass = Int16.Parse(fields[5]);
            short monsterHP = Int16.Parse(fields[6]);
            short monsterSTR = Int16.Parse(fields[7]);
            short monsterDEX = Int16.Parse(fields[8]);
            short monsterCON = Int16.Parse(fields[9]);
            short monsterINT = Int16.Parse(fields[10]);
            short monsterWIS = Int16.Parse(fields[11]);
            short monsterCHA = Int16.Parse(fields[12]);
            
            MonsterInfo toAdd = new MonsterInfo(monsterName, monsterType, monsterAlignment, monsterSize, monsterChallengeRating, monsterArmorClass, monsterHP, monsterSTR, monsterDEX, monsterCON, monsterINT, monsterWIS, monsterCHA);

            toRet.Add(toAdd);
        }
        return toRet;
    }

}