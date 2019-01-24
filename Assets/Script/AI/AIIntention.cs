using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIIntention : MonoBehaviour {
    
    private enum STATUS
    {
        DOWN = 0,
        SIT = 25,
        STAND = 50,
        JUMP = 75
    }

    private enum INFO
    {
        NEUTRAL = 0,
        GUARD = 25,
        ATTACK = 50,
        FEAR = 75
    }

    private enum CHARACTOR
    {
        PLAYER = 0,
        ENEMY,
        SIZE
    }

    private string charName = "";
    [SerializeField, Header("データのファイル名")]
    private string inputDataName = "InputData";
    [SerializeField]
    private string outputDataName = "OutputData";
    [SerializeField, Header("学習回数")]
    private int epochs = 2000;
    [SerializeField, Header("学習率")]
    private float learningRate = 0.1f;

    private PlayerCommand pCommand;
    private PlayerController pController;
    private PlayerCommand eCommand;
    private PlayerController eController;
    private PlayerController[] controller = new PlayerController[(int)CHARACTOR.SIZE];

    private List<float> situation = new List<float>();
    private int intention = -1;
    private int result = 0;
    private List<float> teachValue = new List<float> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    private NumYArray situationDatas = new NumYArray();
    private NumYArray teachDatas = new NumYArray();

    private NeuralNetwork nn;

    private int learningSkipNum = 0;
    private int nowSkip = 0;

    private bool isTrain = false;

    /// <summary>
    /// 初期化
    /// </summary>
	void Start()
    {
        charName = gameObject.name.Replace("IntentionObj", "");
        string filePath = @"Assets/Resources/AIData/" + charName + "/LearningDataCSVw1.csv";

        //csvファイルからデータの読み込み
        NumYArray xData = new NumYArray(NeuralReadCSV.readCSVData("AIData/" + inputDataName));
        NumYArray yData = new NumYArray(NeuralReadCSV.readCSVData("AIData/" + outputDataName));

        //ニューラルネットワークの生成
        nn = new NeuralNetwork(xData.Size[1], 8, yData.Size[1],charName);

        //学習値のデータがないなら初期学習をする
        if (!System.IO.File.Exists(filePath))
        {
            nn.InputData(xData, yData);
            //データを基にトレーニング
            nn.Train(this.epochs, this.learningRate, false);
        }
    }

    /// <summary>
    /// 自身と敵の情報をセットする
    /// </summary>
    public void Initialize(GameObject player, GameObject enemy, int skipNum)
    {
        pCommand = player.GetComponent<PlayerCommand>();
        pController = player.GetComponent<PlayerController>();
        eCommand = enemy.GetComponent<PlayerCommand>();
        eController = enemy.GetComponent<PlayerController>();
        controller[(int)CHARACTOR.PLAYER] = player.GetComponent<PlayerController>();
        controller[(int)CHARACTOR.ENEMY] = enemy.GetComponent<PlayerController>();
        learningSkipNum = skipNum;
    }

    /// <summary>
    /// 状況判断
    /// </summary>
    public void JudgSituation(float dis)
    {
        float[] status = new float[(int)CHARACTOR.SIZE];
        float[] info = new float[(int)CHARACTOR.SIZE];

        //キャラの状態に応じて状況情報を更新
        for (int i = 0; i < (int)CHARACTOR.SIZE; i++)
        {
            switch (controller[i].State)
            {
                case "Stand":
                    status[i] = (float)STATUS.STAND;
                    info[i] = (float)INFO.NEUTRAL;
                    break;
                case "Dash":
                    status[i] = (float)STATUS.STAND;
                    info[i] = (float)INFO.NEUTRAL;
                    break;
                case "Sit":
                    status[i] = (float)STATUS.SIT;
                    info[i] = (float)INFO.NEUTRAL;
                    break;
                case "Jump":
                    status[i] = (float)STATUS.JUMP;
                    info[i] = (float)INFO.NEUTRAL;
                    break;
                case "Guard":
                    status[i] = (float)STATUS.STAND;
                    info[i] = (float)INFO.GUARD;
                    break;
                case "StandGuard":
                    status[i] = (float)STATUS.STAND;
                    info[i] = (float)INFO.GUARD;
                    break;
                case "SitGuard":
                    status[i] = (float)STATUS.SIT;
                    info[i] = (float)INFO.GUARD;
                    break;
                case "Punch":
                    status[i] = (float)STATUS.STAND;
                    info[i] = (float)INFO.ATTACK;
                    break;
                case "Kick":
                    status[i] = (float)STATUS.STAND;
                    info[i] = (float)INFO.ATTACK;
                    break;
                case "SitPunch":
                    status[i] = (float)STATUS.SIT;
                    info[i] = (float)INFO.ATTACK;
                    break;
                case "SitKick":
                    status[i] = (float)STATUS.SIT;
                    info[i] = (float)INFO.ATTACK;
                    break;
                case "Special":
                    status[i] = (float)STATUS.STAND;
                    info[i] = (float)INFO.ATTACK;
                    break;
                case "Damage":
                    status[i] = (float)STATUS.STAND;
                    info[i] = (float)INFO.FEAR;
                    break;
                case "JumpingDamage":
                    status[i] = (float)STATUS.JUMP;
                    info[i] = (float)INFO.FEAR;
                    break;
                case "GuardCrash":
                    status[i] = (float)STATUS.STAND;
                    info[i] = (float)INFO.FEAR;
                    break;
            }

            //ニューラルネットワークで使えるようコンマ2桁まで下げる
            status[i] /= 100.0f;
            info[i] /= 100.0f;
        }

        //状況情報を保存
        situation.Add(dis);
        for (int i = 0; i < (int)CHARACTOR.SIZE; i++)
        {
            situation.Add(status[i]);
            situation.Add(info[i]);
        }
    }

    /// <summary>
    /// 意思決定
    /// </summary>
    public int DecideIntention()
    {
        NumYArray xData = new NumYArray();
        xData.Add(situation);
        //ニューラルネットワークによる結果表示
        NumYArray resultArray = new NumYArray(nn.Predict(xData));
        List<List<float>> resultList = resultArray.Get();

        //ランダムによる意思決定
        float n = Random.Range(0.0f, NumY.Sum(resultArray).Get()[0][0]);
        float offset = 0.0f;
        for (int i = 0; i < resultList[0].Count; i++)
        {
            offset += resultList[0][i];
            if(n <= offset)
            {
                intention = i;
                break;
            }
        }

        return this.intention;
    }

    /// <summary>
    /// 意思と結果を基に教師値を算出
    /// </summary>
    public void CalcTeachData(string result, string state, float dis)
    {
        if(intention == -1)
        {
            return;
        }

        //教師値の算出
        switch (result)
        {
            case "Guard":
                teachValue[intention] = 1.0f;
                break;
            case "WasGuarded":
                if(state == "StandGuard")
                {
                    teachValue[(int)EnemyAI.BEHAVE.swATTACK] = 1.0f;
                    teachValue[(int)EnemyAI.BEHAVE.ssATTACK] = 1.0f;
                }
                else
                {
                    teachValue[intention] = 1.0f;
                }
                break;
            case "Damage":
                switch (state)
                {
                    case "StandGuard":
                        teachValue[(int)EnemyAI.BEHAVE.sGUARD] = 1.0f;
                        break;
                    case "SitGuard":
                        teachValue[(int)EnemyAI.BEHAVE.GUARD] = 1.0f;
                        break;
                    case "Kick":
                        teachValue[(int)EnemyAI.BEHAVE.wATTACK] = 1.0f;
                        break;
                    case "SitKick":
                        teachValue[(int)EnemyAI.BEHAVE.swATTACK] = 1.0f;
                        break;
                    default:
                        teachValue[(int)EnemyAI.BEHAVE.sGUARD] = 1.0f;
                        teachValue[(int)EnemyAI.BEHAVE.GUARD] = 1.0f;
                        break;
                }
                break;
            case "Damaged":
                teachValue[intention] = 1.0f;
                break;
            default:
                nowSkip++;
                if (nowSkip < learningSkipNum)
                {
                    situation = new List<float>();
                    return;
                }
                for (int i = 0; i < teachValue.Count; i++)
                {
                    if (dis <= 0.1f)
                    {
                        teachValue[(int)EnemyAI.BEHAVE.wATTACK] = 1.0f;
                        teachValue[(int)EnemyAI.BEHAVE.sATTACK] = 1.0f;
                        if (dis <= 0.3f)
                        {

                            teachValue[(int)EnemyAI.BEHAVE.swATTACK] = 1.0f;
                            teachValue[(int)EnemyAI.BEHAVE.ssATTACK] = 1.0f;
                        }
                        break;
                    }
                    if (i != (int)EnemyAI.BEHAVE.wATTACK &&
                        i != (int)EnemyAI.BEHAVE.sATTACK &&
                        i != (int)EnemyAI.BEHAVE.swATTACK &&
                        i != (int)EnemyAI.BEHAVE.ssATTACK)
                    {
                        if (i != (int)EnemyAI.BEHAVE.JUMP &&
                            i != (int)EnemyAI.BEHAVE.fJUMP &&
                            i != (int)EnemyAI.BEHAVE.bJUMP)
                        {
                            teachValue[i] = 1.0f;
                        }
                        else
                        {
                            teachValue[i] = 0.3f;
                        }
                    }
                }
                nowSkip = 0;
                break;
        }

        if (situation.Count == 0)
        {
            JudgSituation(dis);
        }

        //状況データと教師データを学習データに追加
        situationDatas.Add(situation);
        teachDatas.Add(teachValue);

        //入っているデータを削除
        situation = new List<float>();
        teachValue = new List<float> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    }

    /// <summary>
    /// 保存したデータを基に学習開始
    /// </summary>
    public bool Learning(bool isNowTrain)
    {
        isTrain = isNowTrain;

        if(isNowTrain)
        {
            isTrain = nn.Train(epochs, learningRate, isNowTrain);
        }
        else
        {
            nn.InputData(situationDatas, teachDatas);

            //入っているデータを削除
            situationDatas = new NumYArray();
            teachDatas = new NumYArray();

            isTrain = nn.Train(epochs, learningRate, isNowTrain);
        }

        if(!isTrain)
        {
            nn.SaveLearningData(charName + "/");
        }

        return isTrain;
    }

    public bool IsTrain { get { return isTrain; } }
}
