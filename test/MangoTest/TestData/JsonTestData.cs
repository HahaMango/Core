using System;
using System.Collections.Generic;
using System.Text;

namespace MangoTest.TestData
{

    public class JsonTestData
    {
        private static IList<object[]> Data = new List<object[]>()
        {
            new object[]
            {
                new
                {
                    Id = 1244523,
                    Title = "mytest"
                },
                @"{""id"":1244523,""title"":""mytest""}"
            },
            new object[]
            {
                new
                {
                    ArticleTitle = "Title",
                    Items = new object[]
                    {
                        new
                        {
                            Name ="name1"
                        },
                        new
                        {
                            Name = "name2"
                        }
                    }
                },
                @"{""articleTitle"":""Title"",""items"":[{""name"":""name1""},{""name"":""name2""}]}"
            },
            new object[]
            {
                new
                {
                    UserName = "罗志祥",
                    ArticleCategoryName = "默认分类",
                    UserInfo = "测试abc"
                },
                @"{""userName"":""罗志祥"",""articleCategoryName"":""默认分类"",""userInfo"":""测试abc""}"
            },
            new object[]
            {
                new
                {
                    MyData = @"~`!@#$%^&*()_-+={}[]:;'<>,"".?/"" "
                },
                @"{""myData"":""~`!@#$%^&*()_-+={}[]:;'<>,\"".?/\"" ""}"
            }
        };

        public static IEnumerable<object[]> MyTestData => Data;
    }
}
