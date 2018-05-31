using Foundation;
using System;
using System.Collections.Generic;
using UIKit;
using System.Linq;

namespace TableofElements
{
    public partial class TableViewController : UITableViewController
    {
        //ORDER: Element, Symbol, Atomic Number, Period
        private string[] elements = { "Actinium,Ac,89,7", "Aluminum,Al,13,3", "Americium,Am,95,7", "Antimony,Sb,51,5",
            "Argon,Ar,18,3", "Arsenic,As,33,4", "Astatine,At,85,6", "Barium,Ba,56,6", "Berkelium,Bk,97,7", "Beryllium,Be,4,2 ",
            "Bismuth,Bi,83,6", "Boron,B,5,2 ", "Bromine,Br,35,4", "Cadmium,Cd,48,5", "Calcium,Ca,20,4", "Californium,Cf,98,7",
            "Carbon,C,6,2 ", "Cerium,Ce,58,6", "Cesium,Cs,55,6", "Chlorine,Cl,17,3", "Chromium,Cr,24,4", "Cobalt,Co,27,4",
            "Copper,Cu,29,4", "Curium,Cm,96,7", "Dubnium,Db,105,7", "Dysprosium,Dy,66,6","Einsteinium,Es,99,7","Erbium,Er,68,6",
            "Europium,Eu,63,6", "Fermium,Fm,100,7", "Flourine,F,9,2 ", "Francium,Fr,87,7", "Gadolinium,Gd,64,6", "Gallium,Ga,31,4",
            "Germanium,Ge,32,4", "Gold,Au,79,6", "Hafnium,Hf,72,6", "Hassium,Hs,108,7", "Helium,He,2,1", "Holmium,Ho,67,6",
            "Hydrogen,H,1,1", "Indium,In,49,5", "Iodine,I,53,5", "Iridium,Ir,77,6", "Iron,Fe,26,4", "Krypton,Kr,36,4", "Lanthanum,La,57,6",
            "Lawrencium,Lr,103,7", "Lead,Pb,82,6", "Lithium,Li,3,2 ", "Lutetium,Lu,71,6", "Magnesium,Mg,12,3", "Manganese,Mn,25,4",
            "Meitnerium,Mt,109,7", "Mendelevium,Md,101,7", "Mercury,Hg,80,6", "Molybdenum,Mo,42,5", "Neilsborium,Ns,107,7",
            "Neodymium,Nd,60,6", "Neon,Ne,10,2 ", "Neptunium,Np,93,7", "Nickel,Ni,28,4", "Niobium,Nb,41,5", "Nitrogen,N,7,2 ",
            "Nobelium,No,102,7", "Osmium,Os,76,6", "Oxygen,O,8,2 ", "Palladium,Pd,46,5", "Phosphorus,P,15,3", "Platinum,Pt,78,6",
            "Plutonium,Pu,94,7", "Polonium,Po,84,6", "Potassium,K,19,4", "Praseodymium,Pr,59,6", "Promethium,Pm,61,6", "Protactinium,Pa,91,7",
            "Radium,Ra,88,7", "Radon,Rn,86,6", "Rhenium,Re,75,6", "Rhodium,Rh,45,5", "Rubidium,Rb,37,5", "Ruthenium,Ru,44,5",
            "Rutherfordium,Rf,104,7", "Samarium,Sm,62,6", "Scandium,Sc,21,4", "Seaborgium,Sg,106,7", "Selenium,Se,34,4", "Silicon,Si,14,3",
            "Silver,Ag,47,5", "Sodium,Na,11,3", "Strontium,Sr,38,5", "Sulfur,S,16,3", "Tantalum,Ta,73,6", "Technetium,Tc,43,5",
            "Tellurium,Te,52,5", "Terbium,Tb,65,6", "Thalium,Tl,81,6", "Thorium,Th,90,7", "Thulium,Tm,69,6", "Tin,Sn,50,5",
            "Titanium,Ti,22,4", "Tungsten,W,74,6", "Uranium,U,92,7", "Vanadium,V,23,4", "Xenon,Xe,54,5", "Ytterbium,Yb,70,6",
            "Yttrium,Y,39,5", "Zinc,Zn,30,4", "Zirconium,Zr,40,5"};

        List<TableItem> elementList = new List<TableItem>();

        public TableViewController(IntPtr handle) : base(handle)
        {
            for (int i = 0; i < elements.Length; i++)
            {
                string[] line = elements[i].Split(',');
                elementList.Add(new TableItem(line[0], line[1], line[2], line[3]));
            }
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            ElementsTable.Source = new TableSource(elementList);
        }
    }

    public class TableItem
    {
        public string Element { get; set; }

        public string Symbol { get; set; }

        public string AtomicNumber { get; set; }

        public string Period { get; set; }


        public UITableViewCellStyle CellStyle
        {
            get { return cellStyle; }
            set { cellStyle = value; }
        }
        protected UITableViewCellStyle cellStyle = UITableViewCellStyle.Default;

        public UITableViewCellAccessory CellAccessory
        {
            get { return cellAccessory; }
            set { cellAccessory = value; }
        }
        protected UITableViewCellAccessory cellAccessory = UITableViewCellAccessory.None;

        public TableItem() { }

        public TableItem(string elm, string sym, string anum, string per)
        {   Element = elm;
            Symbol = sym;
            AtomicNumber = anum; 
            Period = per;  
        }
    }

    public class TableSource : UITableViewSource
    {

        Dictionary<string, List<TableItem>> tableItems;
        protected string cellIdentifier = "TableCell";
        string[] keys;

        public TableSource(List<TableItem> items)
        {
            tableItems = new Dictionary<string, List<TableItem>>();
            foreach(TableItem cel in items)
            {
                if(tableItems.ContainsKey(cel.Element.Substring(0, 1)))
                    tableItems[cel.Element.Substring(0, 1)].Add(cel);
                else
                    tableItems.Add(cel.Element.Substring(0, 1), new List<TableItem>() { cel });

            }
            keys = tableItems.Keys.ToArray();
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return keys.Length;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            //return tableItems.Count;
            return tableItems[keys[section]].Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            // request a recycled cell to save memory  
            UITableViewCell cell = tableView.DequeueReusableCell(cellIdentifier);


            var cellStyle = UITableViewCellStyle.Subtitle;

            // if there are no cells to reuse, create a new one  
            if (cell == null)
            {
                cell = new UITableViewCell(cellStyle, cellIdentifier);
            }

            cell.TextLabel.Text = $" {tableItems[keys[indexPath.Section]][indexPath.Row].Element}, " +
                $"{tableItems[keys[indexPath.Section]][indexPath.Row].Symbol}";
            
            cell.DetailTextLabel.Text = $"Atomic Number: {tableItems[keys[indexPath.Section]][indexPath.Row].AtomicNumber} " +
                $"Period: {tableItems[keys[indexPath.Section]][indexPath.Row].Period}";

            return cell;
        }

        public override String[] SectionIndexTitles(UITableView tableView)
        {
            return keys;
        }
    }
}