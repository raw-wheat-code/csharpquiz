using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;



namespace csharpquiz
{
    public class GenerateGroups
    {
        // Declares file path and sets it equal to root directory of project
        private string filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);

        public GroupList LoadGroupsFromJson()
        {
            // Navigates backwards to the correct directory in the project
            filePath = Directory.GetParent(filePath).FullName;
            filePath = Directory.GetParent(Directory.GetParent(filePath).FullName).FullName;
            filePath += @"\questiongroup.json"; // Then adds the actual file name
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                GroupList groupList = JsonConvert.DeserializeObject<GroupList>(json);
                return groupList;
            }
            return new GroupList();
        }
    }

}