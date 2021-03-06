using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIUContractFinder.Operators
{
    class PathCollector : IDisposable
    {
        private List<string> paths;
        private List<string> filesPaths;
        private readonly string TXT_EXTENSION;
        private readonly string CSV_EXTENSION;

        private IntPtr handle;
        // Other managed resource this class uses.
        private Component component = new Component();
        // Track whether Dispose has been called.
        private bool disposed = false;

        // The class constructor.
        public PathCollector(IntPtr handle)
        {
            paths = new List<string>();
            this.handle = handle;
        }

        // Implement IDisposable.
        // Do not make this method virtual.
        // A derived class should not be able to override this method.
        public void Dispose()
        {
            Dispose(true);
            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        // Dispose(bool disposing) executes in two distinct scenarios.
        // If disposing equals true, the method has been called directly
        // or indirectly by a user's code. Managed and unmanaged resources
        // can be disposed.
        // If disposing equals false, the method has been called by the
        // runtime from inside the finalizer and you should not reference
        // other objects. Only unmanaged resources can be disposed.
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
                // If disposing equals true, dispose all managed
                // and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
                    component.Dispose();
                }

                // Call the appropriate methods to clean up
                // unmanaged resources here.
                // If disposing is false,
                // only the following code is executed.
                CloseHandle(handle);
                handle = IntPtr.Zero;

                // Note disposing has been done.
                disposed = true;
            }
        }

        [System.Runtime.InteropServices.DllImport("Kernel32")]
        private extern static Boolean CloseHandle(IntPtr handle);

        // Use C# destructor syntax for finalization code.
        // This destructor will run only if the Dispose method
        // does not get called.
        // It gives your base class the opportunity to finalize.
        // Do not provide destructors in types derived from this class.
        public PathCollector()
        {
            paths = new List<string>();
            filesPaths = new List<string>();
            TXT_EXTENSION = "txt";
            CSV_EXTENSION = "csv";
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose(false);
        }

        public void AddPath(string p)
        {
            paths.Add(p);
        }

        public string GetPath(int i)
        {
            if (i < paths.Count)
            {
                return paths[i];
            }

            return "";
        }

        /*public bool CollectChildPaths(string p)
        {
            DirectoryInfo di = new DirectoryInfo(p);
            if (di.Exists)
            {
                DirectoryInfo[] dis = di.GetDirectories();
                if (dis.Length > 0)
                {
                    for (int i = 0; i < dis.Length; i++)
                    {
                        paths.Add(dis[i].FullName);
                    }
                    return true;
                }
            }

            return false;
        }*/
        public void SetPaths(List<string> list)
        {
            paths = list;
        }

        public List<string> CollectChildPaths(string p)
        {
            List<string> list = new List<string>();
            DirectoryInfo di = new DirectoryInfo(p);
            if (di.Exists)
            {
                DirectoryInfo[] dis = di.GetDirectories();
                for (int i = 0; i < dis.Length; i++)
                {
                    list.Add(dis[i].FullName);
                }

                if (dis.Length > 0)
                {
                    for (int i = 0; i < dis.Length; i++)
                    {
                        list.AddRange(CollectChildPaths(dis[i].FullName));
                    }
                }
            }
            return list;
        }

        public List<string> GetPaths()
        {
            return paths;
        }

        public List<string> GetFiles()
        {
            return filesPaths;
        }

        public void CollectFilesPaths()
        {
            foreach (string p in paths)
            {
                DirectoryInfo di = new DirectoryInfo(p);
                FileInfo[] fis = di.GetFiles();
                if (fis.Length > 0)
                {
                    for (int i = 0; i < fis.Length; i++)
                    {
                        string fn = fis[i].FullName;
                        
                        if (fn.EndsWith(TXT_EXTENSION) || fn.EndsWith(CSV_EXTENSION))
                        {
                            filesPaths.Add(fis[i].FullName);
                        }
                    }
                }
            }
        }
    }
}
