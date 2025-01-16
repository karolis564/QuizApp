export default function QuizQuestion({
    question,
    options,
    type,
    selectedAnswer,
    onAnswerChange,
  }) {
    return (
      <>
      <h2>{question}</h2>
        <div className="quiz-answers">
        
        {type === "radio" && (
  <ul>
    <div className="radio-options-container">
      {options.map((option, index) => (
        <li key={index} className="radio-option">
          <input
            id={`option-${index}`}
            type="radio"
            name="quiz-option"
            value={option}
            checked={selectedAnswer === option}
            onChange={onAnswerChange}
            className="radio-input"
          />
          <label htmlFor={`option-${index}`} className="radio-label">
            <span className="button-text">{option}</span>
          </label>
        </li>
      ))}
    </div>
  </ul>
)}

   {type === "checkbox" && (
  <ul className="checkbox-options-container">
    {options.map((option, index) => (
      <li key={index}>
        <label id="inp" className="checkbox-label">
          <input
            type="checkbox"
            value={option}
            checked={selectedAnswer.includes(option)}
            onChange={onAnswerChange}
            className="checkbox-input"
          />
          {/* Custom checkbox div */}
          <div
            className={`custom-checkbox ${selectedAnswer.includes(option) ? 'checked' : ''}`}
          ></div>
          <span className="checkbox-text">{option}</span>
        </label>
      </li>
    ))}
  </ul>
)}

          {type === "textbox" && (
  <div className="textbox-container">
    <input
      type="text"
      placeholder="Enter your answer"
      onChange={onAnswerChange}
      className="textbox-input"
    />
  </div>
)}
         </div>
      </>
    );
  }
  