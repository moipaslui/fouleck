using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Connector
{
	public int nodeController;
	public int nodeVictim;
	public bool isActivationConnector;
    public int line = -1;

    public Connector(Node controller, Node victim, bool isActivationConnector, int line = -1)
    {
        nodeController = controller.id;
        nodeVictim = victim.id;
        this.isActivationConnector = isActivationConnector;
        this.line = line;
    }

    public bool Draw(bool activate)
    {
        if (line == -1 || line == QuestEditor.line)
        {
            Color col;

            if (Node.NodeAtID(nodeController) == null || Node.NodeAtID(nodeVictim) == null)
                return false;

            Rect start = Node.NodeAtID(nodeController).rect;
            Rect end = Node.NodeAtID(nodeVictim).rect;

            Vector3 startPos;
            Vector3 endPos;
            Vector3 startTan;
            Vector3 endTan;

            if (activate)
            {
                col = Color.green;

                startPos = new Vector3(start.x + start.width / 2, start.y + start.height);
                endPos = new Vector3(end.x + start.width / 2, end.y);
                startTan = startPos + Vector3.up * Vector3.Distance(startPos, endPos) / 3;
                endTan = endPos + Vector3.down * Vector3.Distance(startPos, endPos) / 3;
            }
            else
            {
                col = Color.red;

                startPos = new Vector3(start.x + start.width, start.y + start.height / 2);
                endPos = new Vector3(end.x, end.y + start.height / 2);
                startTan = startPos + Vector3.right * Vector3.Distance(startPos, endPos) / 3;
                endTan = endPos + Vector3.left * Vector3.Distance(startPos, endPos) / 3;
            }

            Color shadowCol = new Color(0, 0, 0, 0.06f);
            Handles.DrawBezier(startPos, endPos, startTan, endTan, col, null, 1);
            for (int i = 0; i < 3; i++) // Draw a shadow
                Handles.DrawBezier(startPos, endPos, startTan, endTan, shadowCol, null, (i + 1) * 5);
        }
        return true;
    }
}
