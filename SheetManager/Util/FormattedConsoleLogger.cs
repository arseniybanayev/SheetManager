using System;
using System.Collections.Generic;
using System.Text;
using Tesseract;

namespace SheetManager.Util
{
	public class FormattedConsoleLogger
	{
		private const string Tab = "    ";

		private class Scope : DisposableBase
		{
			private readonly int _indentLevel;
			private readonly string _indent;
			private readonly FormattedConsoleLogger _container;

			public Scope(FormattedConsoleLogger container, int indentLevel) {
				_container = container;
				_indentLevel = indentLevel;
				var indent = new StringBuilder();
				for (var i = 0; i < indentLevel; i++)
					indent.Append(Tab);
				_indent = indent.ToString();
			}

			public void Log(string format, object[] args) {
				var message = string.Format(format, args);
				var indentedMessage = new StringBuilder(message.Length + _indent.Length * 10);
				var i = 0;
				var isNewLine = true;
				while (i < message.Length) {
					if (message.Length > i && message[i] == '\r' && message[i + 1] == '\n') {
						indentedMessage.AppendLine();
						isNewLine = true;
						i += 2;
					} else if (message[i] == '\r' || message[i] == '\n') {
						indentedMessage.AppendLine();
						isNewLine = true;
						i++;
					} else {
						if (isNewLine) {
							indentedMessage.Append(_indent);
							isNewLine = false;
						}
						indentedMessage.Append(message[i]);
						i++;
					}
				}

				Console.WriteLine(indentedMessage.ToString());

			}

			public Scope Begin() => new Scope(_container, _indentLevel + 1);

			protected override void Dispose(bool disposing) {
				if (!disposing) return;
				var scope = _container._scopes.Pop();
				if (scope != this)
					throw new InvalidOperationException("Format scope removed out of order.");
			}
		}

		private readonly Stack<Scope> _scopes = new Stack<Scope>();

		public IDisposable Begin(string title = "", params object[] args) {
			Log(title, args);
			var scope = _scopes.Count == 0 ? new Scope(this, 1) : ActiveScope.Begin();
			_scopes.Push(scope);
			return scope;
		}

		public void Log(string format, params object[] args) {
			if (_scopes.Count > 0)
				ActiveScope.Log(format, args);
			else
				Console.WriteLine(format, args);
		}

		private Scope ActiveScope {
			get {
				var top = _scopes.Peek();
				if (top == null) throw new InvalidOperationException("No current scope");
				return top;
			}
		}
	}
}