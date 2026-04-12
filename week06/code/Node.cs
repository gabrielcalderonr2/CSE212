public class Node
{
    public int Data { get; set; }
    public Node? Right { get; private set; }
    public Node? Left { get; private set; }

    public Node(int data)
    {
        this.Data = data;
    }

    public void Insert(int value)
    {
        // TODO Start Problem 1

        // Do not allow duplicates
        if (value == Data)
        {
            return;
        }

        if (value < Data)
        {
            // Insert to the left
            if (Left is null)
                Left = new Node(value);
            else
                Left.Insert(value);
        }
        else
        {
            // Insert to the right
            if (Right is null)
                Right = new Node(value);
            else
                Right.Insert(value);
        }
    }

    public bool Contains(int value)
    {
        // TODO Start Problem 2

        // Check current node
        if (value == Data)
            return true;

        // Search left side
        if (value < Data)
        {
            if (Left is null)
                return false;

            return Left.Contains(value);
        }
        else
        {
            // Search right side
            if (Right is null)
                return false;

            return Right.Contains(value);
        }
    }

    public int GetHeight()
    {
        // TODO Start Problem 4

        // Get height of left and right
        int leftHeight = Left == null ? 0 : Left.GetHeight();
        int rightHeight = Right == null ? 0 : Right.GetHeight();

        // Return max height + 1
        return 1 + Math.Max(leftHeight, rightHeight);
    }
}