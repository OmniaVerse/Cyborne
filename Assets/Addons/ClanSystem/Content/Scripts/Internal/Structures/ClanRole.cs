using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MFPS.Addon.Clan
{
    /// <summary>
    /// ULogin Account role data
    /// </summary>
    [Serializable]
    public class ClanMemberRole
    {
        [Tooltip("The role nice name that will be displayed to the user.")]
        public string RoleName;
        [Tooltip("The role key that will be used in code to identify this role, this should not be changed.")]
        public string RoleKey;
        public Color RoleColor;

        /// <summary>
        /// Get the role prefix text
        /// </summary>
        /// <returns></returns>
        public string GetRolePrefix()
        {
            return $"<color=#{ColorUtility.ToHtmlStringRGBA(RoleColor)}>{RoleName}</color>";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleKey"></param>
        /// <returns></returns>
        public static ClanMemberRoleRef GetRoleRef(string roleKey)
        {
            int roleId = bl_ClanSettings.Instance.roles.FindIndex(x => x.RoleKey.ToLower() == roleKey.ToLower());
            return roleId;
        }
    }

    [Serializable]
    public struct ClanMemberRoleRef
    {

        private int _index;
        private ClanMemberRole _role;

        /// <summary>
        /// Role index
        /// </summary>
        public int RoleId
        {
            get
            {
                return this;
            }
        }

        /// <summary>
        /// Role Data
        /// </summary>
        public ClanMemberRole Role
        {
            get
            {
                if (_role == null) _role = bl_ClanSettings.GetRole(this);
                return _role;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is ClanMemberRoleRef)
            {
                ClanMemberRoleRef playerRef = (ClanMemberRoleRef)obj;
                return _index == playerRef._index;
            }
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return _index;
        }

        public override string ToString()
        {
            return Role == null ? "None" : $"{Role.RoleName}";
        }

        public static implicit operator ClanMemberRoleRef(int value)
        {
            ClanMemberRoleRef result = default(ClanMemberRoleRef);
            result._index = value + 1;
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator bool(ClanMemberRoleRef value)
        {
            return value._index > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator int(ClanMemberRoleRef value)
        {
            return value._index - 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(ClanMemberRoleRef a, ClanMemberRoleRef b)
        {
            return a._index == b._index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(ClanMemberRoleRef a, ClanMemberRoleRef b)
        {
            return a._index != b._index;
        }
    }
}