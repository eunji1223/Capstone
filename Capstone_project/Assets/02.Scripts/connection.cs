using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

public class connection : MonoBehaviour
{
    //서버의 ip와 접근할 user값, 서버 비밀번호가 들어가있음
    private string connectionString = "server=123.212.65.180;user=Client;database=honey;port=3306;password=pedol78787!;";


    //play_log에 입력할 변수들
    string uniquePlayCode = ""; // example play_code
    string user_id = "201902482"; // example user_id
    int mode_id = 1; // example mode_id
    DateTime play_date = DateTime.Now.Date; // example play_date (today's date)

    DateTime gameStartTime;
    DateTime gameEndTime;
    TimeSpan play_time;
    //new TimeSpan(1, 30, 0);  example play_time (1 hour 30 minutes)
    int movement_count = 5; // example movement_count

    //게임에 등장할 동작을 순서대로 저장한 리스트
    List<int> gameList = new List<int> { 1, 2, 3, 4, 5 };

    //게임에서 동작마다의 성공여부를 저장한 리스트
    List<int> gameResult = new List<int> { 1, 0, 1, 1, 0 };


    
    void Start()
    {
        gameStartTime = DateTime.Now;
    }

    void Update()
    {
        // Q 누르면
        if (Input.GetKeyDown(KeyCode.Q))
        {  
            Debug.Log("Q was pressed.");

            gameEndTime = DateTime.Now;
            play_time = gameEndTime - gameStartTime;
            //play_code를 생성&중복체크
            uniquePlayCode = GenerateUniquePlayCode();

            //play_log테이블에 플레이 데이터 입력
            InsertPlayLog(uniquePlayCode, user_id, mode_id, play_date, play_time, movement_count);

            //play_movement테이블에 플레이 데이터 입력
            InsertPlayMovements(uniquePlayCode, gameList, gameResult);

        }
    

        // W 누르면
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("W was pressed.");
            //동작별 등장개수를 리스트에 저장{0번 등장수, 1번 등장수,...,9번 등장수}
            List<int> movement_counts = GetMovementIdCounts();
            

            //동작별 성공개수를 리스트에 저장{0번 성공수, 1번 성공수,...,9번 성공수}
            List<int> success_counts = GetSuccessCountsByMovementId();


            Debug.Log(GetSuccessCountsByMovementId().Count);
            for(int i = 0; i<success_counts.Count; i++){
            Debug.Log(success_counts[i]);
            }
            
        }

        // E 누르면
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E was pressed.");
            //모드에 해당하는 동작을 리스트에 저장
            List<int> MovementIdsByModeId = GetMovementIdsByModeId(mode_id);
        }
    }


//play_code를 UUID방식으로 생성하는 함수(중복확인 기능 포함)
       public string GenerateUniquePlayCode()
    {
        string playCode;
        bool isUnique = false;

        while (!isUnique)
        {
            playCode = Guid.NewGuid().ToString();
            if (!IsPlayCodeExists(playCode))
            {
                isUnique = true;
                return playCode;
            }
        }

        return null; // Should never reach here
    }

    //play_code 중복확인 함수
    private bool IsPlayCodeExists(string playCode)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = "SELECT COUNT(*) FROM play_log WHERE play_code = @playCode";
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@playCode", playCode);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }
    }


//play_log테이블에 게임 플레이 정보를 저장하는 함수
 public void InsertPlayLog(string playCode, string userId, int modeId, DateTime playDate, TimeSpan playTime, int movementCount)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = "INSERT INTO play_log (play_code, user_id, mode_id, date, play_time, movement_count) VALUES (@playCode, @userId, @modeId, @playDate, @playTime, @movementCount)";
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@playCode", playCode);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@modeId", modeId);
                cmd.Parameters.AddWithValue("@playDate", playDate);
                cmd.Parameters.AddWithValue("@playTime", playTime);
                cmd.Parameters.AddWithValue("@movementCount", movementCount);
                
                cmd.ExecuteNonQuery();
            }
        }
    }


//play_movement테이블에 게임 플레이 정보를 저장하는 함수
public void InsertPlayMovements(string playCode, List<int> gameList, List<int> gameResult)
    {
        if (gameList.Count != gameResult.Count)
        {
            throw new ArgumentException("gameList and gameResult must have the same length");
        }

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = "INSERT INTO play_movement (play_code, movement_id, ordiner, success) VALUES (@playCode, @movementId, @ordiner, @success)";
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                for (int i = 0; i < gameList.Count; i++)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@playCode", playCode);
                    cmd.Parameters.AddWithValue("@movementId", gameList[i]);
                    cmd.Parameters.AddWithValue("@ordiner", i);
                    cmd.Parameters.AddWithValue("@success", gameResult[i]);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
    // 동작번호 0~9 의 등장개수를 List로 반환하는 함수
    public List<int> GetMovementIdCounts()
    {
        List<int> counts = new List<int>();
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = "SELECT movement_id, COUNT(*) AS count FROM play_movement WHERE movement_id BETWEEN 0 AND 9 GROUP BY movement_id ORDER BY movement_id";
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    // Initialize counts list with zeroes for movement_id 0 to 9
                    for (int i = 0; i < 10; i++)
                    {
                        counts.Add(0);
                    }
                    // Fill the counts list with actual data from the database
                    while (reader.Read())
                    {
                        int movementId = reader.GetInt32("movement_id");
                        int count = reader.GetInt32("count");
                        counts[movementId] = count;
                    }
                }
            }
        }
        return counts;
    }


    // 동작번호 0~9 의 성공개수를 List로 반환하는 함수
    public List<int> GetSuccessCountsByMovementId()
    {
        List<int> counts = new List<int>();
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = "SELECT movement_id, COUNT(*) AS success_count FROM play_movement WHERE success = 1 AND movement_id BETWEEN 0 AND 9 GROUP BY movement_id ORDER BY movement_id";
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    // Initialize counts list with zeroes for movement_id 0 to 9
                    for (int i = 0; i < 10; i++)
                    {
                        counts.Add(0);
                    }
                    // Fill the counts list with actual data from the database
                    while (reader.Read())
                    {
                        int movementId = reader.GetInt32("movement_id");
                        int successCount = reader.GetInt32("success_count");
                        counts[movementId] = successCount;
                    }
                }
            }
        }
        return counts;
    }


 // int타입의 mode_id값을 입력받아 모드에 해당하는 동작번호 List를 반환하는 함수
    public List<int> GetMovementIdsByModeId(int modeId)
    {
        List<int> movementIds = new List<int>();
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = "SELECT movement_id FROM mode_movement WHERE mode_id = @modeId";
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@modeId", modeId);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int movementId = reader.GetInt32("movement_id");
                        movementIds.Add(movementId);
                    }
                }
            }
        }
        return movementIds;
    }


    //중복확인 함수 이미 있는 id면 true 반환
    public bool IsUserIdExists(string userId)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = "SELECT COUNT(*) FROM User WHERE user_id = @userId";
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@userId", userId);
                int userCount = Convert.ToInt32(cmd.ExecuteScalar());
                return userCount > 0;
            }
        }
    }


    //회원 등록, 가입 함수(중복확인 기능 포함)
    public bool RegisterUser(string userId, string name, string password)
    {
        if (IsUserIdExists(userId))
        {
            return false; // User ID already exists
        }

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = "INSERT INTO User (user_id, name, start_date, password) VALUES (@userId, @name, CURDATE(), @password)";
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@password", password); // Consider hashing the password before storing
                cmd.ExecuteNonQuery();
                return true; // User registered successfully
            }
        }
    }


    //로그인 함수(id, pw 입력하여 둘중 하나라도 잘못되면 false반환)
    public bool ValidateUser(string userId, string password)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = "SELECT COUNT(*) FROM User WHERE user_id = @userId AND password = @password";
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@password", password);
                int userCount = Convert.ToInt32(cmd.ExecuteScalar());
                return userCount > 0;
            }
        }
    }

    /*기타 함수들 모음
    // 데이터베이스 연결 함수
    void TestConnection()
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                Debug.Log("Successfully connected to the database.");
            }
            catch (Exception ex)
            {
                Debug.LogError("Error connecting to the database: " + ex.Message);
            }
        }
    }
    
    // 데이터 읽기 함수 (movement 테이블)
    public void FetchData()
    {
        string query = "SELECT movement_id, movement_name FROM movement";

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            MySqlCommand command = new MySqlCommand(query, conn);
            try
            {
                conn.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Debug.Log("movement_id: " + reader["movement_id"] + ", movement_name: " + reader["movement_name"]);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Debug.LogError("Error fetching data: " + ex.Message);
            }
        }
    }
    
    // 데이터 입력 함수 (movement 테이블)
    public void InsertData(int m_id, string m_name)
    {
        string query = "INSERT INTO movement (movement_id, movement_name) VALUES (@id, @name)";

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            MySqlCommand command = new MySqlCommand(query, conn);
            command.Parameters.AddWithValue("@id", m_id);
            command.Parameters.AddWithValue("@name", m_name);
            try
            {
                conn.Open();
                command.ExecuteNonQuery();
                Debug.Log("Data inserted successfully.");
            }
            catch (Exception ex)
            {
                Debug.LogError("Error inserting data: " + ex.Message);
            }
        }
    }
    
    // 0번 동작(발구르기)의 등장수 반환함수
    public int GetMovementIdZeroCount()
    {
        int count = 0;
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = "SELECT COUNT(*) FROM play_movement WHERE movement_id = @movementId";
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@movementId", 0);
                count = Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        return count;
    }
    
    */
}
