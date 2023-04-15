import React, { useEffect } from "react";
import { questions } from "./data.js";
import Question from "./Question";
import { MdOutlineLibraryBooks } from "react-icons/md";

const Faq = () => {
  return (
    <section id="faq">
      <div className="container faq">
        <div className="u-title" data-aos="fade-up">
          <MdOutlineLibraryBooks color="orangered" size={30} />
          <h2>FAQs</h2>
          <p className="u-text-small">
            Some questions and answers that might help you:
          </p>
        </div>
        <div className="questions">
          {questions.map((question) => (
            <Question
              key={question.id}
              title={question.title}
              answer={question.answer}
            />
          ))}
        </div>
      </div>
    </section>
  );
};

export default Faq;