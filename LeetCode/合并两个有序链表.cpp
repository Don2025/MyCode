/**
 * Definition for singly-linked list.
 * struct ListNode {
 *     int val;
 *     ListNode *next;
 *     ListNode(int x) : val(x), next(NULL) {}
 * };
 */
class Solution {
public:
    ListNode* mergeTwoLists(ListNode* l1, ListNode* l2) {
        if(l1 == NULL) return l2;
        if(l2 == NULL) return l1;
        ListNode* Head = new ListNode(0);   //创建返回链表
        ListNode* ans = Head;
        while(l1 && l2)
        {
            if(l1->val > l2->val)
            {
                ans->next = l2;
                ans = ans->next;
                l2 = l2->next;
            }
            else  
            {
                ans->next = l1;
                ans = ans->next;
                l1 = l1->next;
            }
        }
        while(l1)
        {
            ans->next = l1;
            ans = ans->next;
            l1 = l1->next;
        }
        while(l2)
        {
            ans->next = l2;
            ans = ans->next;
            l2 = l2->next;
        }
        return Head->next;
    }
};