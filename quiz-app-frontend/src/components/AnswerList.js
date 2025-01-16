// AnswerList.js
import React from "react";

const AnswerList = ({ selectedAnswer }) => {
  return (
    <div>
      <p>Your selected answer(s):</p>
      <ul>
        {selectedAnswer.map((answer, index) => (
          <li key={index} style={{ marginBottom: '10px' }}>
            {answer}
          </li>
        ))}
      </ul>
    </div>
  );
};

export default AnswerList;
