using UnityEngine;
using TMPro;

namespace MFPS.Addon.Clan
{
    public class bl_MemberListUI : bl_ClanBase
    {
        public TextMeshProUGUI NameText;
        [SerializeField] private TextMeshProUGUI scoreText = null;
        public GameObject KickButton;
        public GameObject AscendButton;
        public GameObject DesendButton;

        private bl_ClanInfo.ClanMember MemberInfo;
        private bl_MembersWindow membersWindow;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="mc"></param>
        public void Set(bl_ClanInfo.ClanMember info, bl_MembersWindow mc)
        {
            membersWindow = mc;
            MemberInfo = info;
            NameText.text = MemberInfo.GetNameWithRole();
            scoreText.text = info.Score.ToString();
            KickButton.SetActive(false);
            AscendButton.SetActive(false);
            DesendButton.SetActive(false);

#if CLANS
            var localUser = bl_DataBase.Instance.LocalUser;
            ClanMemberRoleRef pr = localUser.Clan.PlayerRole();
            if (pr != 0) // if is not a normal member
            {
                int localRoleID = (int)pr;
                int memberRoleID = (int)MemberInfo.Role;
                if (localRoleID > memberRoleID)
                {
                    if (!ClanSettings.onlyLeaderCanKickMembers)
                    {
                        //don't allow kick ourselves
                        KickButton.SetActive(MemberInfo.ID != localUser.ID);
                    }
                    if (!ClanSettings.onlyLeaderCanPromoteMembers)
                    {
                        DesendButton.SetActive(memberRoleID > 0);
                        if ((localRoleID - memberRoleID) >= 2)//parent ranks can't accent others just one above him.
                        {
                            AscendButton.SetActive(true);
                        }
                    }
                }

                if (ClanSettings.allowTransferLeadership && pr == ClanMemberRole.GetRoleRef("leader"))
                {
                    if (MemberInfo.Role == ClanMemberRole.GetRoleRef("tier2"))
                    {
                        AscendButton.SetActive(true);
                    }
                }
            }

            if (ClanSettings.onlyLeaderCanKickMembers)
            {
                KickButton.SetActive(pr == ClanMemberRole.GetRoleRef("leader") && MemberInfo.ID != localUser.ID);
            }

            if (ClanSettings.onlyLeaderCanPromoteMembers && pr == ClanMemberRole.GetRoleRef("leader") && MemberInfo.ID != localUser.ID)
            {
                AscendButton.SetActive(MemberInfo.Role != ClanMemberRole.GetRoleRef("leader"));
                DesendButton.SetActive(MemberInfo.Role != 0);
            }
#endif
        }

        /// <summary>
        /// 
        /// </summary>
        public void Kick()
        {
            bl_ClanManager.AskComfirmationFor("Kick this member?", () =>
            {
                membersWindow.Kick(MemberInfo, false);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        public void Ascend()
        {
#if CLANS
            if (MemberInfo.Role == ClanMemberRole.GetRoleRef("tier2"))
            {
                bl_ClanManager.AskComfirmationFor("Transfer Leadership to this member?", () =>
                {
                    membersWindow.myClan.TransferLeadership(MemberInfo);
                });
            }
            else
            {
                bl_ClanManager.AskComfirmationFor("Ascend this member?", () =>
                 {
                     membersWindow.ChangeMemberRole(MemberInfo, true);
                 });
            }
#endif
        }

        /// <summary>
        /// 
        /// </summary>
        public void Desend()
        {
            bl_ClanManager.AskComfirmationFor("Descend this member?", () =>
            {
                membersWindow.ChangeMemberRole(MemberInfo, false);
            });
        }
    }
}