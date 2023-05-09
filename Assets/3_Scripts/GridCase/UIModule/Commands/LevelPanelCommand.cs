using TMPro;

namespace GridRunner.Grid.UIModule.Commands
{
    public class LevelPanelCommand
    {

        #region Self Variables

        #region Private Variables

        private readonly TextMeshProUGUI _gridScoreText;
        private int matchCount = 0;

        #endregion

        #endregion

        public LevelPanelCommand(TextMeshProUGUI gridScoreText)
        {
            _gridScoreText = gridScoreText;
        }

        public void SetGridScoreText(int gemValue)
        {
            matchCount += gemValue;
            _gridScoreText.text = "MATCH COUNT \n " + matchCount.ToString();
        }
    }
}
