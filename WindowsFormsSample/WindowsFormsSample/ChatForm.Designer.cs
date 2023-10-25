namespace WindowsFormsSample
{
    partial class ChatForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            addressTextBox = new System.Windows.Forms.TextBox();
            connectButton = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            messagesList = new System.Windows.Forms.ListBox();
            sendButton = new System.Windows.Forms.Button();
            messageTextBox = new System.Windows.Forms.TextBox();
            disconnectButton = new System.Windows.Forms.Button();
            label2 = new System.Windows.Forms.Label();
            nameTextBox = new System.Windows.Forms.TextBox();
            SuspendLayout();
            // 
            // addressTextBox
            // 
            addressTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            addressTextBox.Location = new System.Drawing.Point(77, 12);
            addressTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            addressTextBox.Name = "addressTextBox";
            addressTextBox.Size = new System.Drawing.Size(607, 23);
            addressTextBox.TabIndex = 0;
            addressTextBox.Enter += addressTextBox_Enter;
            // 
            // connectButton
            // 
            connectButton.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            connectButton.Location = new System.Drawing.Point(692, 9);
            connectButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            connectButton.Name = "connectButton";
            connectButton.Size = new System.Drawing.Size(88, 27);
            connectButton.TabIndex = 1;
            connectButton.Text = "Connect";
            connectButton.UseVisualStyleBackColor = true;
            connectButton.Click += connectButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(14, 15);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(52, 15);
            label1.TabIndex = 2;
            label1.Text = "Address:";
            // 
            // messagesList
            // 
            messagesList.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            messagesList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            messagesList.FormattingEnabled = true;
            messagesList.Location = new System.Drawing.Point(18, 68);
            messagesList.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            messagesList.Name = "messagesList";
            messagesList.SelectionMode = System.Windows.Forms.SelectionMode.None;
            messagesList.Size = new System.Drawing.Size(856, 537);
            messagesList.TabIndex = 3;
            messagesList.DrawItem += messagesList_DrawItem;
            // 
            // sendButton
            // 
            sendButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            sendButton.Enabled = false;
            sendButton.Location = new System.Drawing.Point(786, 614);
            sendButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            sendButton.Name = "sendButton";
            sendButton.Size = new System.Drawing.Size(88, 27);
            sendButton.TabIndex = 5;
            sendButton.Text = "Send";
            sendButton.UseVisualStyleBackColor = true;
            sendButton.Click += sendButton_Click;
            // 
            // messageTextBox
            // 
            messageTextBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            messageTextBox.Enabled = false;
            messageTextBox.Location = new System.Drawing.Point(18, 616);
            messageTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            messageTextBox.Name = "messageTextBox";
            messageTextBox.Size = new System.Drawing.Size(761, 23);
            messageTextBox.TabIndex = 4;
            messageTextBox.Enter += messageTextBox_Enter;
            // 
            // disconnectButton
            // 
            disconnectButton.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            disconnectButton.Enabled = false;
            disconnectButton.Location = new System.Drawing.Point(786, 9);
            disconnectButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            disconnectButton.Name = "disconnectButton";
            disconnectButton.Size = new System.Drawing.Size(88, 27);
            disconnectButton.TabIndex = 6;
            disconnectButton.Text = "Disconnect";
            disconnectButton.UseVisualStyleBackColor = true;
            disconnectButton.Click += disconnectButton_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(17, 42);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(42, 15);
            label2.TabIndex = 8;
            label2.Text = "Name:";
            label2.Click += label2_Click;
            // 
            // nameTextBox
            // 
            nameTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            nameTextBox.Location = new System.Drawing.Point(77, 41);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.Size = new System.Drawing.Size(607, 23);
            nameTextBox.TabIndex = 9;
            // 
            // ChatForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(888, 653);
            Controls.Add(nameTextBox);
            Controls.Add(label2);
            Controls.Add(disconnectButton);
            Controls.Add(sendButton);
            Controls.Add(messageTextBox);
            Controls.Add(messagesList);
            Controls.Add(label1);
            Controls.Add(connectButton);
            Controls.Add(addressTextBox);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "ChatForm";
            Text = "SignalR Chat Sample";
            Load += ChatForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox addressTextBox;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox messagesList;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.TextBox messageTextBox;
        private System.Windows.Forms.Button disconnectButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox nameTextBox;
    }
}

