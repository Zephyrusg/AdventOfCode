using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Y2018D8
    {
        public static int Part1()
        {
            LicenseNode rootNode = LicenseNode.GetLicenseNodeFromIndex(0);
            int som = 0;
            foreach (LicenseNode node in LicenseNode.AllLicensenodes)
            {
                foreach (int metaDataEnrty in node.metaData)
                {
                    som += metaDataEnrty;
                }
            }


            return som;
        }

        public static int Part2()
        {
            LicenseNode rootNode = LicenseNode.GetLicenseNodeFromIndex(0);

            return rootNode.Value;


        }

    }
}
