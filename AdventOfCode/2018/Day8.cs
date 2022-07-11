using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2018
{
    internal class Day8
    {
        public static int Day8A()
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

        public static int Day8B()
        {
            LicenseNode rootNode = LicenseNode.GetLicenseNodeFromIndex(0);

            return rootNode.Value;


        }

    }
}
