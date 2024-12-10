using System.Reflection;

namespace AB.MVC.AutoMapperConfig
{
    public static class TipDonusturucu
    {

        public static Hedef Donustur<Kaynak, Hedef>(Kaynak kaynak) where Hedef : class, new()
        {
            Hedef hedef = new Hedef();
            foreach (var p in typeof(Kaynak).GetProperties())
            {
                PropertyInfo property=typeof(Hedef).GetProperty(p.Name);
                property.SetValue(hedef, p.GetValue(kaynak));

            }
            return hedef;
        }
    }
}
