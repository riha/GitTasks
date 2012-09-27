using LibGit2Sharp;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace GitTasks
{
    public class GitVersionTask : Task
    {
        private string id = string.Empty;
        private string path;

        [Output]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        [Required]
        public string Path
        {
            set { path = value; }
            get { return path; }
        }

        public override bool Execute()
        {
            Repository repo;

            try
            {
                repo = new Repository(Path);
            }
            catch (LibGit2Exception)
            {
                Log.LogMessage(string.Concat("An error whiling trying to read git repo. Verify a valid Git repo at ", path));
                return false;
            }
            
            var tip = repo.Head.Tip;

            if (tip == null)
            {
                Log.LogMessage("Couldn't find a current commit, returns empty");
            }
            else
            {
                Id = tip.Sha;
            }
            
            return true;
        }
    }
}
