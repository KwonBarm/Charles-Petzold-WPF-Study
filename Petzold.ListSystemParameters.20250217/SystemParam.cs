namespace Petzold.ListSystemParameters._20250217
{
    internal class SystemParam
    {
        string strName;
        object objValue;

        public string Name
        {
            get { return strName; }
            set { strName = value; }
        }

        public object Value
        {
            get { return objValue; }
            set { objValue = value; }
        }

        public override string ToString()
        {
            return Name + " = " + Value;
        }
    }
}
