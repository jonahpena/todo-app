import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import "./App.css";
import TaskList from "./TaskListScreen/TaskList.js";
import AnimatedIcon from "./IntoScreen/AnimatedIcon";

function App() {
  return (
    <Router>
      <div className="App">
        <Routes>
          <Route path="/" element={<AnimatedIcon />} />
          <Route path="/tasklist" element={<TaskList listId={1} />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;
