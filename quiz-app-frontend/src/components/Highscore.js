import React, { useEffect, useState } from 'react';

const Highscore = () => {
  const [topScores, setTopScores] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchTopScores = async () => {
      try {
        const response = await fetch('http://localhost:5147/api/quiz/top-scores');
        if (!response.ok) {
          throw new Error('Failed to fetch top scores');
        }
        const data = await response.json();
      
        setTopScores(data); 
      } catch (error) {
        setError(error.message);
      } finally {
        setLoading(false);
      }
    };
  
    fetchTopScores();
  }, []);
  
  

  if (loading) {
    return <div>Loading...</div>;
  }

  if (error) {
    return <div>Error: {error}</div>;
  }

  return (
    <div className="quiz-container">
  <h1>Highscore Page</h1>
  
  
  <h2>Top 10 Scores</h2>
  <div className="score-table">
    <div className="score-row">
      <div className="score-cell">Rank</div>
      <div className="score-cell">Email</div>
      <div className="score-cell">Total Score</div>
    </div>
    {topScores.map((score, index) => (
      <div key={index} className="score-row">
        <div className="score-cell">{index + 1}</div>
        <div className="score-cell">{score.email}</div>
        <div className="score-cell">{score.totalScore}</div>
      </div>
    ))}
  </div>
</div>


  
  );
};

export default Highscore;
