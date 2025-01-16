import React, { useState } from 'react';
import Quiz from './Quiz';  // Import your Quiz component
import Header from './Header'; // Header component for switching
import Highscore from './components/Highscore'; // Highscore component
import './styles/styles.css';

const App = () => {
  const [currentPage, setCurrentPage] = useState('quiz'); // State to track the current page

  return (
    <div className="App">
      <Header setPage={setCurrentPage} /> {/* Header for page switching */}

      {/* Conditional rendering based on the currentPage */}
      {currentPage === 'quiz' ? (
        <Quiz />  // Show Quiz component
      ) : (
        <Highscore />  // Show Highscore component
      )}
    </div>
  );
};

export default App;
