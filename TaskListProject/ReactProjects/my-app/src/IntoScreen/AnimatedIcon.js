import React, { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import "./AnimatedIcon.css";

const AnimatedIcon = () => {
  const navigate = useNavigate();

  useEffect(() => {
    const timer = setTimeout(() => {
      navigate("/tasklist");
    }, 1800);

    return () => clearTimeout(timer);
  }, [navigate]);

  return (
    <div className="icon-container">
      <a
        target="doneicon"
        href="https://icons8.com/icon/VFaz7MkjAiu0/done"
        className="bounce-top"
      >
        <img
          src="https://img.icons8.com/clouds/100/null/checkmark--v1.png"
          alt="Done Icon"
          style={{ width: "140px", height: "140px" }}
          className="heartbeat"
        />
      </a>
    </div>
  );
};

export default AnimatedIcon;
