import { render, screen } from "@testing-library/react";
import AddItemForm from "./AddItemForm";

test("renders AddItemForm component", () => {
  render(<AddItemForm onAddItem={() => {}} />);
  expect(screen.getByText(/Enter your task/i)).toBeInTheDocument();
});

// Add more tests to cover the component's functionality
