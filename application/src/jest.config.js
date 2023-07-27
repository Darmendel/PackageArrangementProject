module.exports = {
    testEnvironment: 'node',
    testMatch: ['**/__tests__/**/*.js', '**/?(*.)+(spec|test).js'],
    transformIgnorePatterns: ['/node_modules/(?!three)'],
    type: 'module', 
    preset: '@babel/preset-react',
  };
  