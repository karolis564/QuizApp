import { useState } from 'react';

const useEmailSubmission = (apiBaseUrl) => {
  const [email, setEmail] = useState('');
  const [userEmailId, setUserEmailId] = useState(null);
  const [isEmailSubmitted, setIsEmailSubmitted] = useState(false);

  const validateEmail = (email) => /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email);

  const handleEmailSubmit = async (e) => {
    e.preventDefault();
    if (!validateEmail(email)) {
      alert("Please enter a valid email address.");
      return;
    }

    try {
      const response = await fetch(`${apiBaseUrl}/submit-email`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ Email: email }),
      });
      const data = await response.json();
      if (response.ok) {
        setUserEmailId(data.id);
        setIsEmailSubmitted(true);
      } else {
        alert("Failed to submit email.");
      }
    } catch (error) {
      console.error("Error submitting email:", error);
      alert("An error occurred. Please try again.");
    }
  };

  return { email, setEmail, isEmailSubmitted, userEmailId, handleEmailSubmit };
};

export default useEmailSubmission;
