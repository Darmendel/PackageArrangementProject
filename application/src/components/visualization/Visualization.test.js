import React from 'react';
import { render, screen, fireEvent } from '@testing-library/react';
import Visualization from './Visualization';

describe('Visualization component', () => {
  test('renders without crashing', () => {
    render(<Visualization />);
  });

  test('renders export button', () => {
    render(<Visualization />);
    const exportButton = screen.getByText(/Export arrangements/i);
    expect(exportButton).toBeInTheDocument();
  });

  test('renders solution buttons', () => {
    render(<Visualization />);
    const solution1Button = screen.getByText(/Arrangement plan 1/i);
    const solution2Button = screen.getByText(/Arrangement plan 2/i);
    expect(solution1Button).toBeInTheDocument();
    expect(solution2Button).toBeInTheDocument();
  });

  test('clicking on export button calls handleExportSolution function', () => {
    const handleExportSolution = jest.fn();
    render(<Visualization handleExportSolution={handleExportSolution} />);
    const exportButton = screen.getByText(/Export arrangements/i);
    fireEvent.click(exportButton);
    expect(handleExportSolution).toHaveBeenCalledTimes(1);
  });
});
