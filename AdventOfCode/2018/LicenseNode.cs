using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2018
{
    internal class LicenseNode
    {
        private static string text = File.ReadAllText(".\\2018\\input\\inputDay8.txt");
        public static List<int> Licensedata = text.Split(' ').Select(num => Int32.Parse(num)).ToList();
        public static List<LicenseNode> AllLicensenodes = new List<LicenseNode>();

        public List<LicenseNode> childLicenseNodes;
        public List<int> metaData;
        public int length;

        public int Value 
        { 
            get {
                if (childLicenseNodes.Count == 0)
                {
                    return metaData.Sum();
                }
                else { 
                    
                    int sum = 0;
                    foreach (int metaDataEntry in metaData) {
                        if ((metaDataEntry) <= childLicenseNodes.Count) {
                            sum += childLicenseNodes[metaDataEntry - 1].Value;
                        }
                        
                    }
                    return sum;
                
                }
           
            }
        }

        public LicenseNode(List<LicenseNode> childLicenseNodes, List<int> metaData, int length) 
        { 
            this.childLicenseNodes = childLicenseNodes;
            this.metaData = metaData;
            this.length = length;
        
        }

        public static LicenseNode GetLicenseNodeFromIndex(int index) {
            int childLicensecount = Licensedata[index];
            int metaDataEntriesCount = Licensedata[index+1];
            int length = 2;

            List<LicenseNode> childNodes = new List<LicenseNode>();
            List<int> metaDataList = new List<int>();

            for (int i = 0; i < childLicensecount; i++)
            {

                //childNodes.Add(GetLicenseNodeFromIndex(index + Length));
                LicenseNode childNode = GetLicenseNodeFromIndex(index + length);
                childNodes.Add(childNode);
                length += childNode.length;
            }

            for (int i = index + length; i < (index + length + metaDataEntriesCount); i++)
            {

                //childNodes.Add(GetLicenseNodeFromIndex(index + Length));
                int metaDataEntry = Licensedata[i];
                metaDataList.Add(metaDataEntry);
               
            }
            length += metaDataEntriesCount;
            LicenseNode newlicenseNode = new LicenseNode(childNodes, metaDataList, length);
            AllLicensenodes.Add(newlicenseNode);
            return newlicenseNode; 
        }
    }
}
