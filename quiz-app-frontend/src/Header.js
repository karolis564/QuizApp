import React from 'react';

const Header = ({ setPage }) => {
  return (
    <header className="header">
      <h1>QUIZ</h1>
      <div>
        <button onClick={() => setPage('quiz')} className="header-button">Quiz</button>
        <button onClick={() => setPage('highscore')} className="header-button">Highscore</button>
      </div>
    </header>
  );
};

export default Header;
