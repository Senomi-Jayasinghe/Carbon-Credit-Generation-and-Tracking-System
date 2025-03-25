using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarbonCreditSystem.Model
{
    public class TreeDetails
    {
        public int treeId {  get; set; }
        public string treeLocation { get; set; }
        public string treeName { get; set; }
        public double treeWidth { get; set; }
        public double treeHeight { get; set; }
        public double treeAge { get; set; }
        public Byte[] treePicture { get; set; }
        public string treePictureFormat { get; set; }
        public DateTime entrydate { get; set; }
        public int entryuser {  get; set; }
    }
}