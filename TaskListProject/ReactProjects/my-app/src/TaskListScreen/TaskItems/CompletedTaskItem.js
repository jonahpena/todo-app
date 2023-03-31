import React from "react";
import "../TaskListScreen.css";

function CompletedTaskItem({
  item,
  completedItems,
  handleMoveBackToTask,
  handleRemoveItem,
}) {
  const handleCheckboxChange = (e) => {
    console.log("checkbox changed");
    console.log("item:", item);
    console.log("completedItems:", completedItems);
    if (!e.target.checked) {
      handleMoveBackToTask(item);
    }
  };

  return (
    <li className="completed-task-bubble">
      <div>
        <div className="completed-task-item">
          <label className="filled-in">
            <input
              data-testid="task-checkbox"
              data-index={0}
              type="checkbox"
              className="complete-button"
              checked
              style={{ color: "white" }}
              onChange={handleCheckboxChange}
            />
            <span></span>
          </label>
          <div className="completed-task-item-text">{item.title}</div>
        </div>{" "}
        <div className="completed-delete-icon">
          {" "}
          <div
            data-testid="completed-delete-button"
            className="material-icons"
            onClick={() => handleRemoveItem(item.id)}
          >
            close
          </div>
        </div>
        {/* <span className="completed-time">{item.time}</span> */}
      </div>
    </li>
  );
}

export default CompletedTaskItem;
