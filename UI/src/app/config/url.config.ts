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
        getAll: `user/users?pageNumber={pgNo}&pageSize={pgSize}`,
        update: `user/`,
        delete: `user/{userId}`,
        emailExists: `user/`
    },
    Leave:
    {
        getType: `common/leaveTypes`,
        add: `leave/`,
        get: `leave/user/{userId}`,
        getAll: `leave/department/{departmentId}`,
        updateStatus: `leave/StatusUpdate`,
        report: `leave/report/department/{departmentId}`,
        userReport: `leave/report/user/{userId}`
    },
    Common:
    {
        roles: `common/roles`
    },
    Department:
    {
      get: 'department',
      add: 'department',
      update: 'department',
      delete: 'department/{departmentId}'
    }

}
