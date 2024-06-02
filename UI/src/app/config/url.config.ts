export const apiEndPoint =
{
    Auth:
    {
        //signup: `auth/AddAdmin`,
        signup: `auth/signup`,
        login: `auth/login`
    },
    User:
    {
        getAll: `user/users`,
        update: `user/`,
        delete: `user`,
        emailExists: `user/`
    },
    Leave:
    {
        getType: `common/leaveTypes`,
        add: `leave/`,
        get: `leave/department/{department}/user/{userId}`,
        getAll: `leave/`,
        updateStatus: `leave/StatusUpdate`
    },
    Common:
    {
        roles: `common/roles`
    },
    Department:
    {
      get: 'department'
    }

}
