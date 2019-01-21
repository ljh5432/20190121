using System;
using System.Collections;
using MySql.Data.MySqlClient;

namespace WebAPI2.Modules 
{
    public class Test {
        public string nTitle { set; get; }
        public string nContents { set; get; }
        public string mName { set; get; }
        public string nNo { set; get; }
        
    }

    public static class Query 
    {

        public static ArrayList GetInsert(Test test)
        {
            int mNo = Member_GetSelect(test);

            //Console.WriteLine("성공1 : " + mNo);

            MYsql my = new MYsql();
            string sql = string.Format("insert into Notice (mNo, nTitle, nContents) values ('{0}','{1}','{2}');", mNo, test.nTitle, test.nContents);
            if(my.NonQuery(sql)){
                return GetSelect();
            }
            else 
            {
                return new ArrayList();
            }
            
        }

        public static ArrayList GetUpdate(Test test)
        {
            MYsql my = new MYsql();
            //Console.WriteLine("GetUpdate 진입성공");
            string sql = string.Format("update Notice set nTitle = '{1}', nContents = '{2}' where nNo = {0};", test.nNo, test.nTitle, test.nContents);
            if(my.NonQuery(sql)){
                return GetSelect();
            }
            else 
            {
                return new ArrayList();
            }
        }

        public static ArrayList GetDelete(Test test)
        {
            //Console.WriteLine("GetDelete 진입성공 test.nNo : " + test.nNo);
            MYsql my = new MYsql();
            string sql = string.Format("update Notice set delYn = 'Y' where nNo = {0};", test.nNo);
            if(my.NonQuery(sql)){
                return GetSelect();
            }
            else 
            {
                return new ArrayList();
            }
        }

        public static ArrayList GetSelect()
        {
            MYsql my = new MYsql();
            string sql = "select n.nNo, n.nTitle, n.nContents, m.mName, DATE_FORMAT(n.regDate, '%Y-%m-%d') as regDate, DATE_FORMAT(n.modDate, '%Y-%m-%d') as modDate from Notice as n inner join Member as m on (n.mNo = m.mNo and m.delYn = 'N') where n.delYn = 'N';";
            MySqlDataReader sdr = my.Reader(sql);
            //string result = "";
            ArrayList list = new ArrayList();
            while(sdr.Read())
            {
                Hashtable ht = new Hashtable();
                for(int i = 0; i < sdr.FieldCount; i++)
                {
                    //result += string.Format("{0}\t:\t{1}\t", sdr.GetName(i), sdr.GetValue(i));
                    ht.Add(sdr.GetName(i), sdr.GetValue(i));
                    
                }
                //result += "\n";
                list.Add(ht);
                //Console.WriteLine(list.ToString());
            }
            return list;
        }

        public static int Member_GetSelect(Test test)
        {
            int mNo = 0;
            MYsql my = new MYsql();
            string sql = string.Format("select mNo from Member where mName = '{0}';", test.mName);
            MySqlDataReader sdr = my.Reader(sql);
            //string result = "";
            ArrayList list = new ArrayList();
            while(sdr.Read())
            {
                
                Hashtable ht = new Hashtable();
                for(int i = 0; i < sdr.FieldCount; i++)
                {
                    mNo = Convert.ToInt32(sdr.GetValue(i));
                    //result += string.Format("{0}\t:\t{1}\t", sdr.GetName(i), sdr.GetValue(i));                    
                }                
            }
            return mNo;
        }
    }
}