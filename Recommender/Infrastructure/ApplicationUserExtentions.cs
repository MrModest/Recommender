using Recommender.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recommender.Infrastructure
{
    public static class ApplicationUserExtentions
    {
        private static string pathToAvatarDir = "/userfiles/avatars/";
        private static string noAvatar = "no-avatar.png";

        private static string pathToBgImageDir = "/userfiles/bg-images/";
        private static string noBgImage = "no-bg-image.png";

        public static string GetAvatarPath(this ApplicationUser user)
        {
            return pathToAvatarDir + (user.AvatarImagePath ?? noAvatar);
        }

        public static string GetBgImagePath(this ApplicationUser user)
        {
            return pathToBgImageDir + (user.BackgroundImagePath ?? noBgImage);
        }
    }
}
