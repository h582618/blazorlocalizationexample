using System;

namespace BlazorAppTest.Shared
{
    public class DataLang
    {

        public String language { get; set; }
        public String name { get; set; }
        public String value { get; set; }

        public DataLang()
        {

        }
        public DataLang(String language, string name, string value)
        {
            this.language = language;
            this.name = name;
            this.value = value;
        }


    }

}