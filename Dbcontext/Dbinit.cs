using SocialMedia.Models.DbModels;
using System.Linq;

namespace SocialMedia.Dbcontext
{
    public class Dbinit
    {
        public static void Initialize(MemberContext context)
        {
            //EnsureCreated方法是用來自動建立資料庫
            context.Database.EnsureCreated();

            if (context.Members.Any())
            {
                return;
            }

            //初始化的資料
            //會員
            var member = new Member[]
            {
                new Member{Name="Austin"},
            };

            foreach (Member user in member)
            {
                context.Members.Add(user);
            }

            context.SaveChanges();
            //密碼
            var password = new Password[]
            {
                new Password{MemberID=member.Single(m => m.Name=="Austin").ID,Code="123"},
            };

            foreach (Password pass in password)
            {
                context.Passwords.Add(pass);
            }

            context.SaveChanges();

            //會員資料
            var memberInfo = new MemberInfo[]
            {
                new MemberInfo{MemberID=member.Single(m => m.Name=="Austin").ID,Gender="男",Job="Teacher",State="hungry",Introduce="I like big ass"},
            };

            foreach (MemberInfo userInfo in memberInfo)
            {
                context.MemberInfos.Add(userInfo);
            }

            context.SaveChanges();
            //興趣
            var interests = new Interest[]
            {
                new Interest{Name="釣魚"},
            };

            foreach (Interest interest in interests)
            {
                context.Interests.Add(interest);
            }

            context.SaveChanges();
            //會員興趣
            var memberinters = new MemberInterest[]
            {
                new MemberInterest{MemberID=member.Single(m => m.Name=="Austin").ID,InterestID=interests.Single(k=>k.Name=="釣魚").ID }
            };

            foreach (MemberInterest memberinter in memberinters)
            {
                context.MemberInterests.Add(memberinter);
            }


            context.SaveChanges();
            //類型
            var prs = new Personality[]
            {
                new Personality{Kind="陽光"},
            };

            foreach (Personality pr in prs)
            {
                context.Personalitys.Add(pr);
            }

            context.SaveChanges();
            //偏好類型
            var prefer = new PreferType[]
            {
                new PreferType{ MemberID=member.Single(m => m.Name=="Austin").ID , PersonalityID=prs.Single(p=>p.Kind =="陽光").ID },
            };

            foreach (PreferType prf in prefer)
            {
                context.PreferTypes.Add(prf);
            }

            context.SaveChanges();

            //通訊錄
            var dire = new Directory[]
            {
                new Directory{MemberID=member.Single(m => m.Name=="Austin").ID , ContactList="2,3,4" }
            };

            foreach (Directory dr in dire)
            {
                context.Directorys.Add(dr);
            }

            context.SaveChanges();





        }


    }
}
