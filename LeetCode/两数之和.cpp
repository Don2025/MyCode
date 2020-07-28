#include <bits/stdc++.h>
using namespace std;

class Solution {
public:
    vector<int> twoSum(vector<int>& nums, int target) {
        vector<int> ans(2,-1);    //初始化一个大小为2,值为1的vector,用来存放那俩个整数所在的数组下标
        map<int,int> m;   //map用来记录数字所在的下标,key为数字,value为该数字的所在下标
        for(int i = 0; i < nums.size(); i++)
        {
            if(m.count(target-nums[i]) > 0)   //判断map中是否有和为目标值的那俩个整数
            {
                ans.clear();
                ans.push_back(m[target-nums[i]]);
                ans.push_back(i);
            }
            m[nums[i]] = i;   //记录当前数字的所在下标
        }
        return ans;
    }
};

int main()    //本地测试
{
    Solution s;
    vector<int> nums;
    int a[] = {2,7,11,15};
    for(int i = 0; i < sizeof(a)/sizeof(a[0]); i++)
    {
        nums.push_back(a[i]);
    }
    int target = 9;
    vector<int> v = s.twoSum(nums,target);
    for(int i = 0; i < v.size(); i++)
    {
        cout << v[i] << " ";
    }
    return 0;
}