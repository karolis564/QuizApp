import React, { useState } from 'react';
import QuizQuestion from './components/QuizQuestion';
import AnswerList from './components/AnswerList';
import QuizNavigation from './components/QuizNavigation';
import useFetchQuizzes from './components/useFetchQuizzes';
import useEmailSubmission from './components/useEmailSubmission';
import useQuizSubmission from './components/useQuizSubmission';

const QuizApp = () => {
  const { quizzes } = useFetchQuizzes();
  const { email, setEmail, isEmailSubmitted, userEmailId, handleEmailSubmit } = useEmailSubmission('http://localhost:5147/api/quiz');
  const { score, handleFinishQuiz } = useQuizSubmission('http://localhost:5147/api/quiz', userEmailId);

  const [currentQuestionIndex, setCurrentQuestionIndex] = useState(0);
  const [selectedAnswer, setSelectedAnswer] = useState([]);
  const [selectedAnswerToDataBase, setSelectedAnswerToDataBase] = useState([]);
  const [isQuizFinished, setIsQuizFinished] = useState(false); 
  const handleAnswerChange = (event) => {
    const { value, type, checked } = event.target;
    if (type === 'checkbox') {
      setSelectedAnswer((prev) => (checked ? [...prev, value] : prev.filter((answer) => answer !== value)));
    } else {
      setSelectedAnswer([value]);
    }
  };

  const handlePrevQuestion = () => {
    if (currentQuestionIndex > 0) {
      setCurrentQuestionIndex((prev) => prev - 1);
      setSelectedAnswer(selectedAnswerToDataBase[currentQuestionIndex - 1] || []);
    }
  };
  
  const handleNextQuestion = () => {
    if (currentQuestionIndex < quizzes.length - 1) {
      setSelectedAnswerToDataBase((prev) => {
        const updatedAnswers = [...prev];
        updatedAnswers[currentQuestionIndex] = selectedAnswer;
        return updatedAnswers;
      });
      setSelectedAnswer([]);
      setCurrentQuestionIndex((prev) => prev + 1);
    }
  };

  const handleFinalFinishQuiz = () => {
    handleFinishQuiz(quizzes, [...selectedAnswerToDataBase, selectedAnswer]);

    setSelectedAnswer([]);
    setSelectedAnswerToDataBase([]);
    setCurrentQuestionIndex(0);
    setIsQuizFinished(true);  
  };

  return (
    <div className="quiz-container">
      <h1>Quiz Application</h1>
     
      {!isEmailSubmitted ? (
        <form onSubmit={handleEmailSubmit}>
          <h2>
            Enter your email to start the quiz:
            <input className='form-input' type="email" value={email} onChange={(e) => setEmail(e.target.value)} required />
          </h2>
          <button type="submit">Submit</button>
        </form>
      ) : isQuizFinished ? ( 
        <div>
          <h1>Your Score: {score}</h1>
        </div>
      ) : (
        quizzes.length > 0 && (
          <div>
            <QuizQuestion
              question={quizzes[currentQuestionIndex].question}
              options={quizzes[currentQuestionIndex].options}
              type={quizzes[currentQuestionIndex].questionType}
              selectedAnswer={selectedAnswer}
              onAnswerChange={handleAnswerChange}
            />
            <AnswerList selectedAnswer={selectedAnswer} />
            {currentQuestionIndex < quizzes.length - 1 ? (
              <QuizNavigation
                currentQuestionIndex={currentQuestionIndex}
                totalQuestions={quizzes.length}
                onNextQuestion={handleNextQuestion}
                onPrevQuestion={handlePrevQuestion}
                onFinishQuiz={handleFinalFinishQuiz}
              />
            ) : (
              <button onClick={handleFinalFinishQuiz}>
                Finish Quiz
              </button>
            )}
          </div>
        )
      )}
    </div>
  );
};

export default QuizApp;
