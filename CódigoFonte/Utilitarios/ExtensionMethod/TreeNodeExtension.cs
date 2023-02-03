using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;


public static class TreeNodeExtension
{
    public static void DeselectChildsNodes(this TreeNode node)
    {
        node.Checked = false;        
        if (node.ChildNodes.Count != 0)
        {
            foreach (TreeNode childNode in node.ChildNodes)
            {
                node.DeselectChildsNodes();
            }            
        }
    }
}

