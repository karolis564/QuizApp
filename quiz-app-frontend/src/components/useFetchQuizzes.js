import { useState, useEffect } from 'react';

const useFetchQuizzes = () => {
  const [quizzes, setQuizzes] = useState([]);
  const [error, setError] = useState(null);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    const fetchQuizzes = async () => {
      try {
        const response = await fetch('http://localhost:5147/api/quiz');
        if (!response.ok) {
          throw new Error('Failed to fetch quizzes');
        }
        const data = await response.json();
        setQuizzes(data);
      } catch (err) {
        setError(err);
      } finally {
        setIsLoading(false);
      }
    };

    fetchQuizzes();
  }, []);

  return { quizzes, error, isLoading };
};

export default useFetchQuizzes;
